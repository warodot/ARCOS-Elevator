using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteractable
{
    public void Interact();
    public void LookedAt();
    public void LookedAway();
}
public class Interactor : MonoBehaviour
{
    public Transform interactorSource;
    public float interactRange;

    IInteractable interactableLookedAt;

    void Update()
    {
        HandleRaycast();
        HandleInteractionInput();
    }

    private void HandleRaycast()
    {
        Ray ray = new Ray(interactorSource.position, interactorSource.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, interactRange))
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
        if (hitInfo.collider.gameObject.TryGetComponent(out IInteractable interactable))
        {
            if (interactable != interactableLookedAt)
            {
                ChangeLookedAtInteractable(interactable);
            }
        }
        else
        {
            ClearLookedAtInteractable();
        }
    }

    private void ChangeLookedAtInteractable(IInteractable newInteractable)
    {
        interactableLookedAt?.LookedAway();
        interactableLookedAt = newInteractable;
        interactableLookedAt.LookedAt();
    }

    private void ClearLookedAtInteractable()
    {
        interactableLookedAt?.LookedAway();
        interactableLookedAt = null;
    }

    private void HandleInteractionInput()
    {
        if (Input.GetKeyDown(KeyCode.E) && interactableLookedAt != null)
        {
            interactableLookedAt.Interact();
        }
    }

}
