using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanelBehaviour : MonoBehaviour, IInteractable
{
    public List<GameObject> m_buttonsPrefabs;
    public LayerMask m_newLayerForButtons;

    public void Interact()
    {
        if (ToolController.Instance.CurrentTool != null)
        {
            SearchButtonTool(ToolController.Instance.CurrentTool);
        }
    }

    //Se llama desde un interactable, y se le pasa la lista de los objetos que tenemos actualmente en el inventario.
    public void SearchButtonTool(GameObject equippedTool)
    {
        for (int j = 0; j < m_buttonsPrefabs.Count; j++)
        {
            if (m_buttonsPrefabs[j].name == equippedTool.name)
            {
                PutButtonInElevatorBox(equippedTool);
                break;
            }
            else
            {
                Debug.Log("Botón no encontrado");
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
                // connecting with ring menu system
                RingMenuManager.Instance.RemoveItem(ToolController.Instance.CurrentItem); // bad, but works
                ToolController.Instance.ForceClearEquipped(); // same
                RingMenuManager.Instance.RingLayoutGroup.GetItem(0).Interact(); // this is just embarrassing
                
                button.transform.parent = transform.GetChild(i);
                button.GetComponent<InteractableElevatorButton>().ResetButton();
                button.GetComponent<InteractableElevatorButton>().SetLayerRecursively(LayerToInt(m_newLayerForButtons));
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

    public void LookedAt()
    {
    }

    public void LookedAway()
    {
    }
}
