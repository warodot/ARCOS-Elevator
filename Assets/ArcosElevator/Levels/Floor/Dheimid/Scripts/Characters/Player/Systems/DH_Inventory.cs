using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_Inventory : MonoBehaviour
{
    public static DH_Inventory Instance { get; private set; }

    public List<GameObject> m_tools = new List<GameObject>();
    public GameObject m_toolInHand;
    public GameObject m_pivot;
    public DH_SuitcaseInventory m_suitcase;

    [Space]
    [Header("Detect ideal space for show inventory")]
    public Vector3 boxHalfExtent = new Vector3(0.5f, 0.05f, 1);
    public float forwardOffset;
    public LayerMask m_layerInSuitcase;
    public LayerMask m_layerInHands;

    public static Action<bool> ActiveInventory;

    void Awake() => Instance = this;

    bool canInventory;
    void Update()
    {
        if (canInventory)
        {
            if (DH_GameManager.State == GameStates.Gameplay && !m_suitcase.Opening)
            {
                if (Input.GetKeyDown(KeyCode.Tab)) 
                {
                    ActiveInventory?.Invoke(true);
                    DH_GameManager.State = GameStates.UI;
                }
            }
            else if (DH_GameManager.State == GameStates.UI && !m_suitcase.Opening)
            {
                if (Input.GetKeyDown(KeyCode.Tab)) 
                {
                    ActiveInventory?.Invoke(false);
                    //DH_GameManager.State = GameStates.Gameplay;
                }
            }
        }

        if (m_toolInHand != null)
        {
            if (m_suitcase.InSuitcase) m_toolInHand.SetActive(false);
            else m_toolInHand.SetActive(true);

            if (m_suitcase.canOperate)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {
                    AddToInventory(m_toolInHand);
                    m_toolInHand = null;
                }
            }
        }

        DetectArea();
    }

    void DetectArea()
    {
        //box Detect
        Collider[] cols = Physics.OverlapBox(transform.position + transform.forward * forwardOffset, boxHalfExtent, transform.rotation);

        if (cols.Length > 0) canInventory = false;
        else canInventory = true;
    }

    public void AddToInventory(GameObject tool)
    {
        if (m_suitcase.SpaceFree() > 0)
        {
            if (!m_tools.Contains(tool)) m_tools.Add(tool);
            tool.SetActive(true);
            tool.layer = LayerToInt(m_layerInSuitcase);
            m_suitcase.AddToEmptySpace(tool);
            // DH_UIManager.State = new Dictionary<DH_StateUI, string>();

            DH_UIManager.ActionState?.Invoke(DH_StateUI.AddedToInventory, $"- Se ha añadido {tool.name} al inventario");
        }
    }

    public void UseFromInventory(GameObject tool)
    {
        if (tool != null)
        {   
            if (m_tools.Contains(tool)) Destroy(tool);
            DH_UIManager.ActionState?.Invoke(DH_StateUI.AddedToInventory, $"- Se ha usado {tool.name} del inventario");
        }
    }

    public void RemoveFromInventory(GameObject tool)
    {
        if (m_tools.Contains(tool)) m_tools.Remove(tool);
        m_suitcase.RemoveFromSpace(tool);
        tool.SetActive(false);

        DH_UIManager.ActionState?.Invoke(DH_StateUI.AddedToInventory, $"- Se ha quitado {tool.name} del inventario");
    }

    void OnEnable() => m_suitcase.Tool += ChooseInHand;

    void OnDisable() => m_suitcase.Tool -= ChooseInHand;

    void ChooseInHand(GameObject tool)
    {
        if (tool != null) m_toolInHand = tool;
        ActiveInventory?.Invoke(false);
        
        if (m_toolInHand != null)
        {
            m_toolInHand.SetActive(false);
            m_toolInHand.layer = LayerToInt(m_layerInHands);
            m_toolInHand.transform.position = m_pivot.transform.position;
            m_toolInHand.transform.rotation = m_pivot.transform.rotation;
            m_toolInHand.transform.parent = m_pivot.transform;
        }
    }

    int LayerToInt(LayerMask layerMask)
    {
        int layer = Mathf.RoundToInt(Mathf.Log(layerMask.value, 2));
        return layer;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.forward * forwardOffset, transform.rotation, Vector3.one);
        Gizmos.DrawWireCube(Vector3.zero, boxHalfExtent * 2);
    }
}   
