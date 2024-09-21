using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_Interact : MonoBehaviour
{
    public Transform m_camera;
    public float m_distance;
    public LayerMask m_layer;
    public DH_Inventory m_inventory;

    // Update is called once per frame
    void Update()
    {
        DetectObject();

        if (Input.GetKeyDown(KeyCode.E)) Interact();
    }

    public static bool m_isDetectingObject;
    private RaycastHit m_hit;
    void DetectObject()
    {
        if (Physics.Raycast(m_camera.position, m_camera.forward, out RaycastHit hit, m_distance, m_layer))
        {
            Debug.Log("Encontramos al objetivo");
            if (hit.collider.gameObject.GetComponent<DH_IinteractableObject>() != null)
            {
                m_hit = hit;
                m_isDetectingObject = true;
                Debug.DrawRay(m_camera.position, m_camera.forward * m_distance, Color.red);
                Debug.Log("El objetivo tiene la interface");
            }
        }
        else
        {
            m_hit = new RaycastHit();
            m_isDetectingObject = false;
            Debug.DrawRay(m_camera.position, m_camera.forward * m_distance, Color.white);
        }
    }

    void Interact()
    {
        if (m_hit.collider != null) 
        {
            m_hit.collider.GetComponent<DH_IinteractableObject>().Interact();

            if (m_hit.collider.gameObject.CompareTag("Tool")) 
            {
                m_inventory.AddToInventory(m_hit.collider.gameObject.name);
                Destroy(m_hit.collider.gameObject);
            }
        }
    }
}
