using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IInteractable
{
    void Interact();
    void LookedAt();
    void LookedAway();
}

public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;

    public IInteractable interactableLookedAt;

    [Header("Layer and Tag Options")]
    public LayerMask interactableLayerMask;
    
    public bool useTags = false;
    public string interactableTag = "Interactable";

    [Header("Events")]
    public UnityEvent<IInteractable> onInteractedWithInteractable;
    public UnityEvent<IInteractable> onLookedAtInteractable;
    public UnityEvent<IInteractable> onStoppedLookingAtInteractable;

    public virtual void InteractedWithInteractable(IInteractable interactable)
    {
        interactable?.Interact();  // Null check first
        onInteractedWithInteractable?.Invoke(interactable);
    }

    private void HandleLookEvent(IInteractable interactable, bool isLookedAt)
    {
        if (isLookedAt)
        {
            onLookedAtInteractable?.Invoke(interactable);
            interactable?.LookedAt();
        }
        else
        {
            onStoppedLookingAtInteractable?.Invoke(interactableLookedAt);
            interactable?.LookedAway();
        }
    }

    void Update()
    {
        HandleRaycast();
        HandleInteractionInput();
    }

    private void HandleRaycast()
    {
        Ray ray = new Ray(interactorSource.position, interactorSource.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange, interactableLayerMask))
        {
            ProcessRaycastHit(hitInfo);
        }
        else
        {
            ClearLookedAtInteractable();
        }
    }

    private void ProcessRaycastHit(RaycastHit hitInfo)
    {
        if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable newInteractable))
        {
            // Check tag if "useTags" is enabled
            if (useTags && !hitInfo.collider.CompareTag(interactableTag))
            {
                ClearLookedAtInteractable(); // Doesn't match the tag, clear interactable
                return;
            }

            if (newInteractable != interactableLookedAt)
            {
                ChangeLookedAtInteractable(newInteractable);
            }
        }
        else
        {
            ClearLookedAtInteractable();
        }
    }

    private void ChangeLookedAtInteractable(IInteractable newInteractable)
    {
        HandleLookEvent(interactableLookedAt, false);  // Stopped looking at previous interactable
        interactableLookedAt = newInteractable;
        HandleLookEvent(newInteractable, true);        // Looking at the new interactable
    }

    private void ClearLookedAtInteractable()
    {
        if (interactableLookedAt != null)
        {
            HandleLookEvent(interactableLookedAt, false); // Stopped looking at the current interactable
            interactableLookedAt = null;
        }
    }

    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactableLookedAt != null)
        {
            InteractedWithInteractable(interactableLookedAt);
        }
    }

    public virtual GameObject GetInteractableLookedAt()
    {
        return (interactableLookedAt as MonoBehaviour)?.gameObject;
    }
}