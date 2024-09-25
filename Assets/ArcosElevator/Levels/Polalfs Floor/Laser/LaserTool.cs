using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LaserTool : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private Helper m_helper;
     
    [Header("Laser Settings")]
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private float range = 5f;
    [SerializeField] private Transform shootPos;


    private Camera cam;
    private LineRenderer laserVisual;
    private InteractableObject interactableObject;
    void Start()
    {
        cam = Camera.main;
        laserVisual = GetComponent<LineRenderer>();
    }

    
    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, ray.direction * range, Color.green);
        if (Physics.Raycast(ray, out RaycastHit hit, 10, interactMask))
        {
            InteractableObject newInter = hit.collider.GetComponent<InteractableObject>();
            if (newInter != interactableObject) ChangeLook(newInter);

            interactableObject.LookedAt();
            ShowTypeOfInteract(interactableObject);
            
        }
        else
        {
            if(interactableObject != null)
            {
                interactableObject.LookedAway();
                interactableObject = null;
            }
        }

        if (Input.GetMouseButton(0))
        {
            laserVisual.enabled = true;
            laserVisual.SetPosition(0, shootPos.position);

            if (Physics.Raycast(ray, out RaycastHit _hit, range))
            {
                laserVisual.SetPosition(1, _hit.point);
                Debug.Log(_hit.point);
                if (_hit.collider.TryGetComponent(out InteractableObject interactable))
                {
                    m_helper.SetMove(interactable.offset.position);
                    //m_helper.SetInteractable(interactable);

                }
                m_helper.SetMove(_hit.point);
            }
            else laserVisual.SetPosition(1, transform.position + cam.transform.forward * range);
        }
        else
        {
            laserVisual.enabled = false;
        }

        
    }

    private void ShowTypeOfInteract(InteractableObject interactable)
    {
        text.text = interactable.ShowType().ToString();
    }
    private void ChangeLook(InteractableObject newInteract)
    {
        interactableObject.LookedAway();
        interactableObject = newInteract;
        interactableObject.LookedAt();
    }

}
