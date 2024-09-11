using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LaserTool : Tool
{
    [SerializeField] private TMP_Text text;
    
     
    [Header("Laser Settings")]
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private float range = 5f;
    [SerializeField] private Transform shootPos;


    private Camera cam;
    private LineRenderer laserVisual;
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
            Debug.Log("aaaaaaaaaaaa " + hit.collider.gameObject.name);
            
            ShowTypeOfInteract(hit.collider.GetComponent<InteractableObject>());
        }


        if (Input.GetMouseButton(0))
        {
            laserVisual.enabled = true;
            laserVisual.SetPosition(0, shootPos.position);

            if (Physics.Raycast(ray, out hit, range, interactMask))
            {
                laserVisual.SetPosition(1, hit.point);
                Debug.Log("Contacto");
                //SetHelp(hit.point);
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

}
