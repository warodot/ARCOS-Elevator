using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_ElevatorBoxTest : MonoBehaviour, DH_IinteractableObject
{
    public List<GameObject> m_prefabs;
    public LayerMask m_newLayerForButtons;

    public void Interact(){}

    public void SearchButtonInInventory(List<GameObject> objects)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            for (int j = 0; j < m_prefabs.Count; j++)
            {
                if (m_prefabs[j].name == objects[i].name)
                {
                    Debug.Log("Lo tenemos señor");
                    PutButtonInElevatorBox(objects[i]);
                    break;
                }
                else 
                {
                    Debug.Log("No lo tenemos señor.");
                }
            }
        }
    }

    public void PutButtonInElevatorBox(GameObject button)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == button.name)
            {
                button.transform.position = transform.GetChild(i).position;
                button.transform.rotation = transform.GetChild(i).rotation;
                button.transform.parent = transform.GetChild(i);
                button.layer = LayerToInt(m_newLayerForButtons);
                break;
            }
        }
    }

    public int LayerToInt(LayerMask m_layer)
    {
        int layer = Mathf.RoundToInt(Mathf.Log(m_layer.value, 2));
        return layer;
    }
}
