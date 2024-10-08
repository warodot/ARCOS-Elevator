using System.Reflection;
using UnityEditor;
using UnityEngine;
using System;
using DH_Attributes;

[CustomEditor(typeof(MonoBehaviour), true)]
public class DH_SuitcaseButton : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var methods = target.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
        foreach (var method in methods)
        {
            var attributes = method.GetCustomAttributes(typeof(ButtonAttribute), true);
            if (attributes.Length > 0)
            {
                EditorGUILayout.Space(5);
                if (GUILayout.Button(method.Name))
                {
                    method.Invoke(target, null);
                }
            }
        }
    }
}
