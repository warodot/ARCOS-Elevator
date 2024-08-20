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

    public void Interact()
    {
        Debug.Log("Interacted with object");
        onInteract.Invoke();
    }

    public void LookedAt()
    {
        Debug.Log("Looked at");
        onLookedAt.Invoke();
    }

    public void LookedAway()
    {
        Debug.Log("Looked away");
        onLookedAway.Invoke();
    }
}
