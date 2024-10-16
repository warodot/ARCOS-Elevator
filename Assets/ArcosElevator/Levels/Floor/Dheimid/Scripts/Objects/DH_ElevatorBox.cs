using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DH_ElevatorBox : MonoBehaviour, DH_IinteractableObject
{
    public GameObject m_button;
    public GameObject m_zoneToButton;

    public UnityEvent m_actionToNotButton;

    public void Interact()
    {
        if (DH_Inventory.Instance.m_tools.Contains(m_button))
        {
            m_button.tag = "Untagged";
            m_button.transform.parent = null;
            m_button.transform.position = m_zoneToButton.transform.position;
            m_button.transform.rotation = m_zoneToButton.transform.rotation;
        }
        else
        {
            m_actionToNotButton?.Invoke();
        }
    }
}
