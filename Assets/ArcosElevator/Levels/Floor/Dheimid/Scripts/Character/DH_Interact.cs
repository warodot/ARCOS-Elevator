using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_Interact : MonoBehaviour
{
    public Transform m_camera;
    public float m_distance;
    public LayerMask m_layer;
    public DH_Inventory m_inventory;

    public static event Action<bool> IsDetecting;

    // Update is called once per frame
    void Update()
    {
        DetectObject();

        if (m_hit.collider != null) DetectSurface();
        if (Input.GetKeyDown(KeyCode.E)) Interact();
    }

    public bool canDetect;
    public static bool m_isDetectingObject;
    public static string m_message;
    private RaycastHit m_hit;
    float distance;
    void DetectObject()
    {
        if (DH_GameManager.State == GameStates.Gameplay)
        {
            if (Physics.Raycast(m_camera.position, m_camera.forward, out RaycastHit hit, distance, m_layer))
            {
                if (hit.collider.gameObject.GetComponent<DH_IinteractableObject>() != null)
                {
                    Detecting(hit);
                    Debug.DrawRay(m_camera.position, m_camera.forward * distance, Color.red);
                }
                else 
                {
                    NotDetecting(m_distance);
                    Debug.DrawRay(m_camera.position, m_camera.forward * distance, Color.white);
                }
            }
            else
            {
                NotDetecting(m_distance);
                Debug.DrawRay(m_camera.position, m_camera.forward * distance, Color.white);
            }
        }
        else
        {
            NotDetecting(0);
        }
    }

    void Detecting(RaycastHit hit)
    {
        m_hit = hit;
        distance = hit.distance + 0.1f;
        m_isDetectingObject = true;

        if (!canDetect)
        {
            Debug.Log("Aqui hay algo");
            IsDetecting?.Invoke(true);
            canDetect = true;
        }
    }

    void NotDetecting(float newDistance)
    {
        m_hit = new RaycastHit();
        distance = newDistance;
        m_isDetectingObject = false;

        if (canDetect)
        {
            IsDetecting?.Invoke(false);
            Debug.Log("No hay nada");
            canDetect = false;
        } 
    }

    void DetectSurface()
    {
        switch (m_hit.collider.tag)
        {
            case "DH_Door": m_message = "Abrir Puerta"; break;
            case "DH_Tool": m_message = "Tomar objeto"; break;
            case "DH_Mirilla": m_message = "Intentar mirar a través"; break;
            case "DH_Npc": m_message = "Hablar"; break;
            case "DH_FurnitureDoor": m_message = "Abrir puerta de estante"; break;
            default: m_message = "Interactuar"; break;
        }
    }

    void Interact()
    {
        if (m_hit.collider != null) 
        {
            if (m_hit.collider.gameObject.CompareTag("DH_Tool")) 
            {
                m_inventory.AddToInventory(m_hit.collider.gameObject.name);
                m_hit.collider.gameObject.SetActive(false);
            }
            else if (m_hit.collider.CompareTag("DH_Door"))
            {
                if (m_hit.collider.GetComponent<DH_Door>().m_key != null)
                {
                    if (m_inventory.m_tools.Contains(m_hit.collider.GetComponent<DH_Door>().m_key.name))
                    {
                        m_hit.collider.GetComponent<DH_Door>().UnlockDoor();
                    }
                }
            }

            m_hit.collider.GetComponent<DH_IinteractableObject>().Interact();
        }
    }
}
