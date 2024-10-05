using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_Inventory : MonoBehaviour
{
    public List<GameObject> m_tools = new List<GameObject>();
    public GameObject m_toolInHand;
    public GameObject m_pivot;
    public DH_InventoryBag m_bag;

    public static Action<bool> ActiveInventory;

    void Update()
    {
        if (DH_GameManager.State == GameStates.Gameplay)
        {
            if (Input.GetKeyDown(KeyCode.Tab)) 
            {
                ActiveInventory?.Invoke(true);
                DH_GameManager.State = GameStates.UI;
            }
        }
        else if (DH_GameManager.State == GameStates.UI)
        {
            if (Input.GetKeyUp(KeyCode.Tab)) 
            {
                ActiveInventory?.Invoke(false);
                DH_GameManager.State = GameStates.Gameplay;
            }
        }
    }

    public void AddToInventory(GameObject tool)
    {
        if (m_bag.SpaceFree() > 0)
        {
            if (!m_tools.Contains(tool)) 
            {
                m_tools.Add(tool);
                m_bag.AddToSpace(tool);
                Debug.Log($"Se ha añadido {tool} al invenrario");
            }
        }
    }

    public void UseFromInventory(GameObject tool)
    {
        if (m_tools.Contains(tool)) RemoveFromInventory(tool);
        else Debug.Log("No tienes la herramienta che");
    }

    public void RemoveFromInventory(GameObject tool)
    {
        if (m_tools.Contains(tool)) m_tools.Remove(tool);
    }

    void OnEnable() => DH_InventoryBag.Tool += ChooseInHand;

    void OnDisable() => DH_InventoryBag.Tool -= ChooseInHand;

    void ChooseInHand(GameObject tool)
    {
        if (tool != null) m_toolInHand = tool;
        ActiveInventory?.Invoke(false);
        if (DH_GameManager.State != GameStates.Gameplay) DH_GameManager.State = GameStates.Gameplay;
    }
}   
