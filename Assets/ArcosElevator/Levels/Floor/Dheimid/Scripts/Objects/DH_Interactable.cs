using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DH_Interactable : MonoBehaviour, DH_IinteractableObject
{
    public UnityEvent m_interactEvent;
    
    public void Interact()
    {
        m_interactEvent?.Invoke();
    }

    void OnValidate()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
    }
}
