using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_Inventory : MonoBehaviour
{
    public List<string> m_tools = new List<string>();

    public void AddToInventory(string tool)
    {
        if (!m_tools.Contains(tool)) m_tools.Add(tool);
        Debug.Log($"Se ha añadido {tool} al invenrario");
    }

    public void UseFromInventory(string tool)
    {
        if (m_tools.Contains(tool)) RemoveFromInventory(tool);
        else Debug.Log("No tienes la herramienta che");
    }

    public void RemoveFromInventory(string tool)
    {
        if (m_tools.Contains(tool)) m_tools.Remove(tool);
    }
}   
