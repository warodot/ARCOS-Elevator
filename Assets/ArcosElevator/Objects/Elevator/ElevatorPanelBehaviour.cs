using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorPanelBehaviour : MonoBehaviour, IInteractable
{
    public List<GameObject> m_buttonsPrefabs;
    public LayerMask m_newLayerForButtons;

    public void Interact()
    {
        SearchButtonTool(RingMenuManager.Instance.Items);
    }

    //Se llama desde un interactable, y se le pasa la lista de los objetos que tenemos actualmente en el inventario.
    public void SearchButtonTool(List<Item> itemList)
    {
        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].Prefab == null)
            {
                continue; // Skips checking item if prefab field is empty
            }

            for (int j = 0; j < m_buttonsPrefabs.Count; j++)
            {
                if (m_buttonsPrefabs[j].name == itemList[i].Prefab.name)
                {
                    PutButtonInElevatorBox(itemList[i]);
                    break;
                }
                else
                {
                    Debug.Log("Botón no encontrado");
                }
            }
        }

    }

    //Recibe el botón, compara su nombre con todos los hijos del box y si encuentra uno con el mismo nombre, 
    //copia pos, rot, y se asigna como nuevo hijo.
    void PutButtonInElevatorBox(Item button)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name == button.Prefab.name)
            {
                GameObject newButton = Instantiate(button.Prefab);
                RingMenuManager.Instance.RemoveItem(button);
                RingMenuManager.Instance.RefreshLayout();
                
                if (ToolController.Instance.CurrentItem == button)
                {
                    // connecting with ring menu system
                    GameObject equippedTool = ToolController.Instance.CurrentTool;
                    ToolController.Instance.ForceClearEquipped();
                    Destroy(equippedTool);
                    RingMenuManager.Instance.RingLayoutGroup.GetItem(0).Interact(); // this is just embarrassing
                }

                newButton.transform.parent = transform.GetChild(i);
                newButton.GetComponent<InteractableElevatorButton>().ResetButton();
                newButton.GetComponent<InteractableElevatorButton>().SetLayerRecursively(LayerToInt(m_newLayerForButtons));
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
