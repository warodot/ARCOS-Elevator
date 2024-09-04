using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This is the base class for tools, inherit from this class to make a new tool.

public abstract class Tool : MonoBehaviour
{
    public string toolName;
    public Sprite icon;
    public GameObject model;

    public class ToolFunction
    {
        public KeyCode activationKey;
        public System.Action method;
    }

    public List<ToolFunction> toolFunctions = new List<ToolFunction>();

    // Helper method to create and add a ToolFunction
    protected void AddToolFunction(KeyCode key, System.Action action)
    {
        ToolFunction newFunction = new ToolFunction
        {
            activationKey = key,
            method = action
        };

        toolFunctions.Add(newFunction);
    }

    void Update()
    {
        foreach (var function in toolFunctions)
        {
            if (Input.GetKeyDown(function.activationKey))
            {
                function.method?.Invoke();
            }
        }
    }
}
