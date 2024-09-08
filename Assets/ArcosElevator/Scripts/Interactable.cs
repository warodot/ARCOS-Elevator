using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Interactable : MonoBehaviour, IInteractable
{
    [Header("Events")]
    public UnityEvent onInteract;
    public UnityEvent onLookedAt;
    public UnityEvent onLookedAway;

    public virtual void Interact()
    {
        onInteract.Invoke();
    }

    public virtual void LookedAt()
    {
        onLookedAt.Invoke();
    }

    public virtual void LookedAway()
    {
        onLookedAway.Invoke();
    }
}
