using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DH_Interactable : MonoBehaviour, DH_IinteractableObject
{
    public UnityEvent m_interactEvent;
    public bool onlyOne;
    
    public void Interact()
    {
        m_interactEvent?.Invoke();
        if (onlyOne) GetComponent<BoxCollider>().enabled = false;
    }
}
