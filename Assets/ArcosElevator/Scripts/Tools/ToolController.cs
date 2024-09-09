using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    // I don't really recommend doing this too much, but I can't be bothered to think of a better way right now.
    public static ToolController Instance { get; private set; }
    public GameObject currentTool;

    private void Awake()
    {
        Instance = this;
    }


    public void EquipTool(Item item)
    {
        if (currentTool != null)
        {
            Destroy(currentTool);
        }

        if (item.Prefab != null)
        {
            currentTool = Instantiate(item.Prefab, transform);
        }
        
    }
}
