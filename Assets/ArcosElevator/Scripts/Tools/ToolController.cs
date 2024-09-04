using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    public Tool equippedTool;

    public void EquipTool(Tool tool)
    {
        equippedTool = tool;
        // setup model, ui updates, etc.
    }
}
