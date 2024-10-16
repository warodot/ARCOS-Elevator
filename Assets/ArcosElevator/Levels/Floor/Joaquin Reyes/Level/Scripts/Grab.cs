using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    // Distancia a la que el objeto se colocará frente a la cámara
    public float holdDistance = 2.5f;
    public float grabSpeed = 10f;

    // Capa de los objetos agarrables
    public LayerMask grabbableLayer;

    // Referencia al objeto que estamos agarrando
    private Rigidbody grabbedObject;

    // Posición de la cámara (usualmente es la del jugador)
    public Camera playerCamera;

    void Start()
    {
        // Obtenemos la cámara principal (que será la que realiza el Raycast)
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (grabbedObject == null)  
            {
                TryGrabObject();
            }
            else  
            {
                ReleaseObject();
            }
        }

        if (grabbedObject != null)
        {
            HoldObject();
        }
    }

    // Método para intentar agarrar un objeto
    void TryGrabObject()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // Detectamos si hemos golpeado un objeto en la capa grabbable
        if (Physics.Raycast(ray, out hit, holdDistance, grabbableLayer))
        {
            // Si el objeto tiene un Rigidbody, lo agarramos
            if (hit.rigidbody != null)
            {
                grabbedObject = hit.rigidbody;

                // Desactivamos la gravedad y las físicas para que quede flotando
                grabbedObject.useGravity = false;
                
                
            }
        }
    }

    // Método para mantener el objeto flotando frente a la cámara
    void HoldObject()
    {
        // Calculamos la posición frente a la cámara
        Vector3 holdPosition = playerCamera.transform.position + playerCamera.transform.forward * holdDistance;

        // Movemos el objeto suavemente hacia la posición deseada
        grabbedObject.transform.position = Vector3.Lerp(grabbedObject.transform.position, holdPosition, Time.deltaTime * grabSpeed);
    }

    // Método para soltar el objeto
    void ReleaseObject()
    {
        // Rehabilitamos la gravedad y las físicas del objeto
        grabbedObject.useGravity = true;
        grabbedObject.isKinematic = false;

        // Liberamos la referencia
        grabbedObject = null;
    }
}
