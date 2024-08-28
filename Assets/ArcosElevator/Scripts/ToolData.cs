using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

[CreateAssetMenu(fileName = "ToolData", menuName = "ArcosElevator/Tool", order = 1)]
public class ToolData : ScriptableObject
{
    public string toolName;
    public Image icon;
    public Vector3 position;
    public Mesh mesh;
}
