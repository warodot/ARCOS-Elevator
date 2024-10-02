using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Events")]
    public UnityEvent onInteract;
    public UnityEvent onLookedAt;
    public UnityEvent onLookedAway;

    private Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }


    public virtual void Interact()
    {
        onInteract.Invoke();
    }

    public virtual void LookedAt()
    {
        onLookedAt.Invoke();
        outline.enabled = true;
    }

    public virtual void LookedAway()
    {
        onLookedAway.Invoke();
        outline.enabled = false;
    }
}
