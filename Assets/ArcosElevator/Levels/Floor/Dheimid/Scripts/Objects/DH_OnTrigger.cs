using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DH_OnTrigger : MonoBehaviour
{
    public UnityEvent m_onEnter;
    public UnityEvent m_onExit;

    void OnTriggerEnter(Collider other)
    {
        m_onEnter?.Invoke();
    }

    void OnTriggerExit(Collider other)
    {
        m_onExit?.Invoke();
    }
}
