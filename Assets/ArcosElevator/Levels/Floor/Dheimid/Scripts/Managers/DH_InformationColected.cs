using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DH_InformationColected : MonoBehaviour
{
    List<string> m_dataRecolect = new List<string>();
    public UnityEvent m_actionToColect;

    public void AddInformation(string info)
    {
        if (!m_dataRecolect.Contains(info)) m_dataRecolect.Add(info);

        Debug.Log(m_dataRecolect.Count);

        if (m_dataRecolect.Count == 3) m_actionToColect?.Invoke();
    }
}
