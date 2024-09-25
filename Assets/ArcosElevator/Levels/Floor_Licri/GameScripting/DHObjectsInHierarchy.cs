using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;

[InitializeOnLoad]
public class DHObjectsInHierarchy : MonoBehaviour
{
    private static Dictionary<int, (Color, Color, Font)> objectStyles = new Dictionary<int, (Color, Color, Font)>();
    static DHObjectsInHierarchy()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        Undo.undoRedoPerformed += RepaintHierarchyWindow; // Ensure updates on undo/redo
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID);

        if (obj != null && objectStyles.ContainsKey(instanceID))
        {
            if ((obj as GameObject).GetComponent<DHObjectsInHierarchy>())
            {
                var styles = objectStyles[instanceID];
                HierarchyBackground(selectionRect, styles.Item1, styles.Item2, styles.Item3, obj.name);
            }
            else // The DHObjectsInHierarchy component has been removed from the gameObject
            {
                objectStyles.Remove(instanceID);
            }
        }
    }

    private static void HierarchyBackground(Rect selectionRect, Color backgroundColor, Color textColor, Font font, string name)
    {
        Rect offsetRect = new Rect(selectionRect.position, selectionRect.size);
        Rect bgRect = new Rect(selectionRect.x, selectionRect.y, selectionRect.width + 50, selectionRect.height);

        EditorGUI.DrawRect(bgRect, backgroundColor);
        GUIStyle labelStyle = new GUIStyle(EditorStyles.label)
        {
            normal = new GUIStyleState() { textColor = textColor },
            font = font
        };
        GUI.Label(offsetRect, name, labelStyle);
    }

    private static void RepaintHierarchyWindow()
    {
        EditorApplication.RepaintHierarchyWindow();
    }

    public Color backgroundColor = Color.grey;
    public Color textColor = Color.white;
    public Font customFont;

    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        int instanceID = gameObject.GetInstanceID();

        if (!objectStyles.ContainsKey(instanceID))
        {
            objectStyles.Add(instanceID, (backgroundColor, textColor, customFont));
        }
        else if (objectStyles[instanceID] != (backgroundColor, textColor, customFont))
        {
            objectStyles[instanceID] = (backgroundColor, textColor, customFont);
        }

        EditorApplication.RepaintHierarchyWindow(); // Ensure the hierarchy window is updated
    }

    private void OnDestroy()
    {
        RemoveStyle();
    }

    private void RemoveStyle()
    {
        int instanceID = gameObject.GetInstanceID();
        if (objectStyles.ContainsKey(instanceID))
        {
            objectStyles.Remove(instanceID);
        }

        EditorApplication.RepaintHierarchyWindow(); // Ensure the hierarchy window is updated
    }
}
#endif
