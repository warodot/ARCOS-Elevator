using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(LineRenderer))]
public class Laser : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI text;
    //[SerializeField] private Helper helper;

    [Header("Laser Settings")]
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private float range = 5f;

    private Camera cam;
    private LineRenderer laserVisual;
    
    void Start()
    {
        cam = Camera.main;
        laserVisual = GetComponent<LineRenderer>();
    }

    
    void Update()
    {
        Vector3 rayOri = cam.ViewportToWorldPoint(transform.position);
        RaycastHit hit;
        Ray ray = new Ray(rayOri, cam.transform.forward);
        Debug.DrawRay(transform.position, ray.direction * range, Color.green);
        if(Physics.Raycast(ray, out hit, range, interactMask))
        {
           
            ShowTypeOfInteract(hit.collider.GetComponent<InteractableObject>());
        }


        if (Input.GetMouseButton(0))
        {
            laserVisual.enabled = true;
            laserVisual.SetPosition(0, transform.position);

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
        //text.text = interactable.ShowType().ToString();
        Debug.Log(interactable.typeOfInteract);
    }

    //private void SetHelp(Vector3 pointToM)
    //{
    //    helper.SetMove(pointToM);
    //}
}
