using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ControllerDoctorAI : MonoBehaviour
{
    [Header("Variables del agente")] 
    [SerializeField] private NavMeshAgent agente;
    [SerializeField] private Transform cabezaDelAgente;

    [Header("Raycast y detecciones")]
    [SerializeField] private float areaDetection;
    [SerializeField] private GameObject positionReference;
    private Vector3 raycastPosition;
    public LayerMask targetNameDetection;
    public float smoothRotationOnEnter;
    public float smoothRotationOnExit;

    [Header("Objetos de interacciones")]
    [SerializeField] private Transform mirarObjetivo;
    [SerializeField] private bool estaMirandoAlObjetivo;

    void Detection()
    {
        raycastPosition = positionReference.transform.position;

        estaMirandoAlObjetivo = false;

        Collider[] hitColliders = Physics.OverlapSphere(raycastPosition, areaDetection, targetNameDetection);

        foreach (Collider hitCollider in hitColliders)
        {
            Vector3 directionPlayer = hitCollider.transform.position - raycastPosition;
            Ray ray = new Ray(raycastPosition, directionPlayer);
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.CompareTag("Player"))  //Cuando el objetivo entre en este radio, puedes hacer que pase algo con esta condicion :O
                {
                    Debug.Log("Doctor esta detectando al jugador");
                    Quaternion rotacionCabezaObjetivo = Quaternion.LookRotation(mirarObjetivo.position - cabezaDelAgente.position);
                    cabezaDelAgente.rotation = Quaternion.Lerp(cabezaDelAgente.rotation, rotacionCabezaObjetivo, smoothRotationOnEnter);

                    estaMirandoAlObjetivo = true;
                    break;
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(positionReference.transform.position, areaDetection);
    }


    private void Update()
    {
        Detection();


        if(!estaMirandoAlObjetivo)  //En esta condicion, si el agente deja de detectar al objetivo que estaba mirando, puedes hacer que suceda algo :O
        {
            cabezaDelAgente.localRotation = Quaternion.Lerp(cabezaDelAgente.localRotation, Quaternion.identity, smoothRotationOnExit);
        }
    }
}
