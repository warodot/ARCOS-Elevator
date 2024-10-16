using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DH_Interactable : MonoBehaviour, DH_IinteractableObject
{
    public UnityEvent m_interactEvent;
    public bool onlyOne;
    public LayerMask m_layer;
    
    public void Interact()
    {
        m_interactEvent?.Invoke();
        if (onlyOne) GetComponent<BoxCollider>().enabled = false;
    }

    public void ChangeLayer() => gameObject.layer = LayerToInt(m_layer);

    int LayerToInt(LayerMask layerMask)
    {
        int layer = Mathf.RoundToInt(Mathf.Log(layerMask.value, 2));
        return layer;
    }
}
