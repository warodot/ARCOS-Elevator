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
        Debug.Log("Interacted with object " + gameObject.name);
        onInteract.Invoke();
    }

    public void LookedAt()
    {
        Debug.Log("Looked at " + gameObject.name);
        onLookedAt.Invoke();
    }

    public void LookedAway()
    {
        Debug.Log("Looked away from " + gameObject.name);
        onLookedAway.Invoke();
    }
}
