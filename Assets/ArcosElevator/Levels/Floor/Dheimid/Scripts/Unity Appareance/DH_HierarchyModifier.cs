using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DH_HierarchyModifier : MonoBehaviour
{
    private static Dictionary<int, (Color, Color, Color, string, Sprite)> objectStyles = new Dictionary<int, (Color, Color, Color, string, Sprite)>();
    public static GameObject m_gameObject;
    static DH_HierarchyModifier()
    {
        EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        Undo.undoRedoPerformed += RepaintHierarchyWindow;
        EditorApplication.hierarchyChanged += OnHierarchyChanged;
    }

    private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
    {
        var obj = EditorUtility.InstanceIDToObject(instanceID);

        if (obj != null && objectStyles.ContainsKey(instanceID))
        {
            if ((obj as GameObject).GetComponent<DH_HierarchyModifier>())
            {
                var styles = objectStyles[instanceID];
                HierarchyBackground(selectionRect, styles.Item1, styles.Item3, styles.Item4, styles.Item5);
            }
            else
            {
                objectStyles.Remove(instanceID);
            }
        }
    }

    private static void HierarchyBackground(Rect selectionRect, Color startColor, Color textColor, string displayName, Sprite customIcon)
    {
        // Dibujar el fondo gradiente
        DrawGradientBackground(selectionRect, startColor);

        // Ajustar la posición del rectángulo del texto para respetar el icono predeterminado
        Rect textRect = new Rect(selectionRect.x + 18, selectionRect.y, selectionRect.width - 18, selectionRect.height);

        // Dibujar el texto
        GUIStyle labelStyle = new GUIStyle(EditorStyles.label)
        {
            fixedHeight = 15,
            normal = new GUIStyleState() { textColor = textColor },
        };
        GUI.Label(textRect, displayName, labelStyle);

        // Dibujar un icono personalizado si existe
        if (customIcon != null)
        {
            Rect iconRect = new Rect(selectionRect.x, selectionRect.y, 16, 16);
            GUI.DrawTexture(iconRect, customIcon.texture);
        }
    }

    private static void OnHierarchyChanged()
    {
        List<int> keys = new List<int>(objectStyles.Keys);

        foreach (var key in keys)
        {
            var gameObject = EditorUtility.InstanceIDToObject(key) as GameObject;
            if (gameObject != null)
            {
                string currentName = gameObject.name;
                if (objectStyles[key].Item4 != currentName)
                {
                    objectStyles[key] = (objectStyles[key].Item1, objectStyles[key].Item2, objectStyles[key].Item3, currentName, objectStyles[key].Item5);
                }
            }
        }

        RepaintHierarchyWindow();
    }

    static void DrawGradientBackground(Rect rect, Color baseColor, float startGradientFactor = 0.8f)
    {
        int width = (int)rect.width;
        int startGradientPosition = Mathf.RoundToInt(width * startGradientFactor);
        int gradientWidth = width - startGradientPosition;

        Texture2D gradientTexture = new Texture2D(gradientWidth, 1);

        for (int i = 0; i < gradientWidth; i++)
        {
            float alpha = 1.0f - (float)i / gradientWidth;
            Color gradientColor = new Color(baseColor.r, baseColor.g, baseColor.b, alpha);
            gradientTexture.SetPixel(i, 0, gradientColor);
        }
        gradientTexture.Apply();

        Rect solidRect = new Rect(rect.x, rect.y, startGradientPosition, rect.height);
        EditorGUI.DrawRect(solidRect, baseColor);

        Rect gradientRect = new Rect(rect.x + startGradientPosition, rect.y, gradientWidth, rect.height);
        GUI.DrawTexture(gradientRect, gradientTexture);
    }

    private static void RepaintHierarchyWindow()
    {
        EditorApplication.RepaintHierarchyWindow();
    }

    public Color backgroundColorStart = Color.grey;
    public Color textColor = Color.white;
    public Sprite customIcon; // Añadir la opción para un icono personalizado

    private void Reset()
    {
        OnValidate();
    }

    private void OnValidate()
    {
        int instanceID = gameObject.GetInstanceID();
        string displayName = gameObject.name;

        if (!objectStyles.ContainsKey(instanceID))
        {
            objectStyles.Add(instanceID, (backgroundColorStart, Color.clear, textColor, displayName, customIcon));
        }
        else if (objectStyles[instanceID] != (backgroundColorStart, Color.clear, textColor, displayName, customIcon))
        {
            objectStyles[instanceID] = (backgroundColorStart, Color.clear, textColor, displayName, customIcon);
        }

        EditorApplication.RepaintHierarchyWindow();
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

        EditorApplication.RepaintHierarchyWindow();
    }
}
