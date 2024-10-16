using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.Rendering;
using UnityEditor;

public class LaserTool : MonoBehaviour
{
    [SerializeField] private Helper m_helper;

    [Header("Visuals")]
    [SerializeField] private TMP_Text m_actionPrim;
    [SerializeField] private TMP_Text m_actionSec;
    public Material m_light;
    public Color m_lightColorOn;
    public Color m_lightColorOff;

    [Header("Laser Settings")]
    [SerializeField] private LayerMask interactMask;
    [SerializeField] private float range = 5f;
    [SerializeField] private Transform shootPos;

    private Camera cam;
    private LineRenderer laserVisual;
    private InteractableObject interactableObject;
    void Start()
    {
        m_helper = FindAnyObjectByType<Helper>();
        cam = Camera.main;
        m_actionPrim.text = "";
        m_actionSec.text = "";
        laserVisual = GetComponent<LineRenderer>();
        ChangeLightColor(m_lightColorOff);
    }
   

    void Update()
    {

        ShootRaycast();


        if (m_helper.Box != null) m_actionSec.text = "Soltar caja";
        else m_actionSec.text = "Inventario vacio";
        #region Laser encendido
        if (Input.GetMouseButton(0))
        {
            DrawLaser(0);
            laserVisual.SetPosition(0, shootPos.position);
        }
        else if(Input.GetMouseButton(1))
        {
            DrawLaser(1);
            laserVisual.SetPosition(0, shootPos.position);
        }
        else
        {

            laserVisual.enabled = false;
            m_light.color = m_lightColorOff;
            ChangeLightColor(m_lightColorOff);
        }
        #endregion

    }

    private void DrawLaser(int _action)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        laserVisual.enabled = true;
        
        m_light.color = m_lightColorOn;
        ChangeLightColor(m_lightColorOn);

        if (Physics.Raycast(ray, out RaycastHit _hit, range))
        {
            laserVisual.SetPosition(1, _hit.point);
            if (_action == 0)
            {
                //Debug.Log(_hit.point);
                if (_hit.collider.TryGetComponent(out InteractableObject interactable))
                {
                    m_helper.SetMove(interactable.transform.position);
                    m_helper.SetInteractable(interactable);
                }
                else
                {
                    m_helper.SetMove(_hit.point);
                    laserVisual.SetPosition(1, transform.position + cam.transform.forward * range);
                }
                
            }
            else if (_action == 1)
            {
                m_helper.SetMove(_hit.point);
                m_helper.SetDrop(_hit.point);
            }
        }
        laserVisual.SetPosition(1, transform.position + cam.transform.forward * range);
    }

    private void ChangeLightColor(Color color)
    {
        m_light.color = color;
        m_light.SetColor("_EmissionColor", color * 5);
    }
    private void ShootRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawRay(transform.position, ray.direction * range, Color.green);
        if (Physics.Raycast(ray, out RaycastHit hit, range, interactMask))
        {
            ProcessRaycastHit(hit);
            ShowTypeOfInteract(hit.collider.GetComponent<InteractableObject>());
        }
        else ClearLookeInteractable();

    }
    private void ProcessRaycastHit(RaycastHit hit)
    {
        if (hit.collider.TryGetComponent(out InteractableObject interObject))
        { 
            if (interObject != interactableObject) ChangeLook(interObject);
            
        }
        else ClearLookeInteractable();
    }

    private void ClearLookeInteractable()
    {
        if(interactableObject == null) return;
        m_actionPrim.text = "";
        interactableObject.LookedAway();
        interactableObject = null;
    }

    private void ShowTypeOfInteract(InteractableObject interactable)
    {
        if (interactable == null) return;   
        m_actionPrim.text = interactable.ShowType().ToString();
        if (interactable.ShowType() == TypeOfInteract.Input) m_actionPrim.text = "Presionar";
        else m_actionPrim.text = "Agarrar";
    }
    private void ChangeLook(InteractableObject newInteract)
    {
        if(interactableObject == null) interactableObject = newInteract;
       
        interactableObject.LookedAway();
        interactableObject = newInteract;
        interactableObject.LookedAt();

    }
}
