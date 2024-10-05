using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_ElevatorBoxTest : MonoBehaviour, DH_IinteractableObject
{
    //IMPORTANTE:
    //El gameobject que contengan este script debe tener hijos con el mismo nombre de cada botón,
    //Y esos gameobjects hijos serán los que indiquen la posicion y rotación hacia dónde irá cada botón.

    public List<GameObject> m_buttonsPrefabs;
    public LayerMask m_newLayerForButtons;

    public void Interact(){}

    //Se llama desde un interactable, y se le pasa la lista de los objetos que tenemos actualmente en el inventario.
    public void SearchButtonInInventory(List<GameObject> objects) 
    {
        for (int i = 0; i < objects.Count; i++)
        {
            for (int j = 0; j < m_buttonsPrefabs.Count; j++)
            {
                if (m_buttonsPrefabs[j].name == objects[i].name)
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

    //Recibe el botón, compara su nombre con todos los hijos del box y si encuentra uno con el mismo nombre, 
    //copia pos, rot, y se asigna como nuevo hijo.
    void PutButtonInElevatorBox(GameObject button)
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

    //Función para asignar y comparar layers de manera rápida.
    public int LayerToInt(LayerMask m_layer)
    {
        int layer = Mathf.RoundToInt(Mathf.Log(m_layer.value, 2));
        return layer;
    }
}
