using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine; // Asegúrate de que Cinemachine esté en tus using

public class Enemigo : MonoBehaviour
{
    public Transform player; // Referencia al transform del jugador
    public float moveSpeed = 2.0f; // Velocidad de movimiento del ángel
    public float stopDistance = 2.0f; // Distancia mínima antes de detenerse cerca del jugador
    public float detectionRange = 20.0f; // Rango en el cual el ángel puede ver al jugador

    private Renderer angelRenderer; // Renderer del ángel para comprobar si está a la vista de la cámara
    private Rigidbody rb; // Referencia al Rigidbody del ángel
    private CinemachineVirtualCamera virtualCamera; // Referencia a la cámara virtual de Cinemachine

    void Start()
    {
        angelRenderer = GetComponent<Renderer>();
        rb = GetComponent<Rigidbody>();
        virtualCamera = FindObjectOfType<CinemachineVirtualCamera>(); // Encuentra la cámara virtual en la escena
    }

    void Update()
    {
        if (IsCameraLookingAtAngel())
        {
            // El jugador está mirando al ángel, no hacer nada
            return; 
        }
        else
        {
            MoveTowardsPlayer(); // Mueve el ángel hacia el jugador si no está siendo mirado
        }
    }

    // Comprueba si la cámara está mirando al ángel
    bool IsCameraLookingAtAngel()
    {
        if (virtualCamera == null) return false; // Asegúrate de que la cámara virtual esté asignada

        Vector3 directionToAngel = (transform.position - virtualCamera.transform.position).normalized;
        float dotProduct = Vector3.Dot(virtualCamera.transform.forward, directionToAngel);

        // El ángel es visible si el dotProduct es negativo (cámara mirando hacia el ángel)
        return dotProduct < 0 && IsAngelVisibleFromCamera(); // Verifica si el ángel está dentro del campo de visión
    }

    // Comprueba si el ángel está visible desde la cámara
    bool IsAngelVisibleFromCamera()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(virtualCamera.GetComponent<Camera>());

        return GeometryUtility.TestPlanesAABB(planes, angelRenderer.bounds);
    }

    // Mueve el ángel hacia el jugador utilizando Rigidbody
    void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance && distanceToPlayer <= detectionRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 moveForce = direction * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + moveForce); // Mueve el ángel utilizando el Rigidbody
        }
    }

    // Dibuja un Gizmo para el rango de detección en el editor
    void OnDrawGizmos()
    {
        // Configurar el color del Gizmo
        Gizmos.color = Color.red; // Puedes cambiar el color si lo deseas
        // Dibuja una esfera que representa el rango de detección
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}
