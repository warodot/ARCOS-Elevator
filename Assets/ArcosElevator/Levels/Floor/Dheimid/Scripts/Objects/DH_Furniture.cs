using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DH_Furniture : MonoBehaviour
{
    public List<DH_FurnitureDoor> m_furniteDoors;
     DH_FurnitureDoor m_currentDoor;

    void Update()
    {
        for (int i = 0; i < m_furniteDoors.Count; i++)
        {
            if (m_furniteDoors[i].isOpen)
            {
                if (m_currentDoor != null && m_currentDoor != m_furniteDoors[i]) m_currentDoor.isOpen = false; 
                m_currentDoor = m_furniteDoors[i];
            }
        }
    }
}
