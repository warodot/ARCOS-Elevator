using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DH_InformationColected : MonoBehaviour
{
    int num = 0;
    public UnityEvent m_actionToColect;
    public UnityEvent m_secondAction;
    public float waitTime;

    public void AddInformation()
    {
        num++;
        Debug.Log(num);

        if (num == 4) Invoke(nameof(InvokeAction), waitTime);
    }
        
    void InvokeAction() 
    {
        m_actionToColect?.Invoke();
        Invoke(nameof(InvokeSecondAction), 0.2f);
    }

    void InvokeSecondAction()
    {
        m_secondAction?.Invoke();
    }
}
