using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DH_InformationColected : MonoBehaviour
{
    int num = 0;
    public UnityEvent m_actionToColect;

    public void AddInformation()
    {
        num++;
        Debug.Log(num);

        if (num == 3) m_actionToColect?.Invoke();
    }
}
