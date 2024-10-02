using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ControllerIA : MonoBehaviour
{
    public enum DetectionMode
    {
        OverlapSphere,
        VisionCone
    }

    [Header("Metodo de deteccion")]
    public DetectionMode detectionMode;

    [Header("Variables del agente")] 
    [SerializeField] private NavMeshAgent agente;
    [SerializeField] private Transform cabezaDelAgente;
    [SerializeField] private Animator agentAnim;
    [SerializeField] private Animator animSprite1;
    [SerializeField] private Animator animSprite2;

    [Header("Raycast y detecciones")]
    [SerializeField] private float areaDetection;
    [SerializeField] private float maxDistance;
    [SerializeField] private float fieldOfView;
    [SerializeField] private GameObject positionReference;
    private Vector3 raycastPosition;
    public LayerMask targetNameDetection;
    public float smoothRotationOnEnter;
    public float smoothRotationOnExit;
    public float velocidadAlCaminar;
    public float velocidadAlCorrer;

    [Header("Objetos de interacciones")]
    [SerializeField] private Transform mirarObjetivo;
    [SerializeField] private bool estaMirandoAlObjetivo;
    [SerializeField] private bool estaMirandoAlPlayer;
    [SerializeField] private UnityEvent tocandoInteractuable;
    public bool agarradoPorElPlayer;
    public Transform objetoASeguirDelPlayer;

    [Header("Patrones de movimiento")]
    [SerializeField] private List<Transform> destinos;
    [SerializeField] private int destinoActual;
    [SerializeField] private float tiempoDeEsperaPorPunto;
    [SerializeField] private float tiempoDeReaccionPorInteractuable;
    public Transform interactuablePosition;

    private void Update()
    {
        if(agarradoPorElPlayer == true)
        {
            transform.position = Vector3.Lerp(transform.position, objetoASeguirDelPlayer.position, 0.05f); //Sigue la posicion de la variable objetoASeguirDelPlayer de manera suave

            Quaternion rotacionCuerpoObjetivo = Quaternion.LookRotation(mirarObjetivo.position - cabezaDelAgente.position);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotacionCuerpoObjetivo, smoothRotationOnEnter); //Sigue la rotacion del player de manera suave
        }

        if(agarradoPorElPlayer == false)
        {
            agente.isStopped = false;
        }

        DetectionTarget();

        if (agente.velocity.magnitude > 0.1f)
        {
            agentAnim.SetBool("Caminando", true);
        }

        else
        {
            agentAnim.SetBool("Caminando", false);
        }

        if(agente.velocity.magnitude > 2)
        {
            agentAnim.SetBool("Corriendo", true);
        }
        else
        {
            agentAnim.SetBool("Corriendo", false);
        }


        if (estaMirandoAlPlayer == false)  //En esta condicion, si el agente deja de detectar al objetivo que estaba mirando, puedes hacer que suceda algo :O
        {
            cabezaDelAgente.localRotation = Quaternion.Lerp(cabezaDelAgente.localRotation, Quaternion.identity, smoothRotationOnExit);
            estaMirandoAlPlayer = false;
        }

    }

    void DetectionTarget()
    {
        raycastPosition = positionReference.transform.position;

        switch(detectionMode)
        {
            case DetectionMode.OverlapSphere:

                Collider[] hitColliders = Physics.OverlapSphere(raycastPosition, areaDetection, targetNameDetection);

                foreach (Collider hitCollider in hitColliders)
                {
                    Vector3 directionPlayer = hitCollider.transform.position - raycastPosition;
                    Ray ray = new Ray(raycastPosition, directionPlayer);
                    RaycastHit hitInfo;

                    if (Physics.Raycast(ray, out hitInfo))
                    {
                        if (hitInfo.collider.CompareTag("Player"))  //Cuando el objetivo entre en este radio, puedes hacer que pase algo en esta condicion :O
                        {
                            Debug.Log("Doctor esta detectando al jugador");
                            Quaternion rotacionCabezaObjetivo = Quaternion.LookRotation(mirarObjetivo.position - cabezaDelAgente.position);
                            cabezaDelAgente.rotation = Quaternion.Lerp(cabezaDelAgente.rotation, rotacionCabezaObjetivo, smoothRotationOnEnter);
                            break;
                        }
                    }
                }
                break;

            case DetectionMode.VisionCone:

                estaMirandoAlPlayer = false;

                Collider[] coneColliders = Physics.OverlapSphere(raycastPosition, maxDistance, targetNameDetection);

                foreach (Collider coneCollider in coneColliders)
                {
                    Vector3 directionToPlayer = (coneCollider.transform.position - raycastPosition).normalized;
                    float angleToPlayer = Vector3.Angle(cabezaDelAgente.forward, directionToPlayer);

                    if (angleToPlayer < fieldOfView / 2)
                    {
                        Ray ray = new Ray(raycastPosition, directionToPlayer);
                        RaycastHit hitInfo;

                        if (Physics.Raycast(ray, out hitInfo, maxDistance))
                        {
                            if (hitInfo.collider.CompareTag("Player"))
                            {
                                estaMirandoAlPlayer = true;
                                if(estaMirandoAlPlayer && estaMirandoAlObjetivo == false)
                                {
                                    Debug.Log("Detectando al jugador con cono de visión");
                                    Quaternion rotacionCabezaObjetivo = Quaternion.LookRotation(mirarObjetivo.position - cabezaDelAgente.position);
                                    cabezaDelAgente.rotation = Quaternion.Lerp(cabezaDelAgente.rotation, rotacionCabezaObjetivo, smoothRotationOnEnter);

                                    Vector3 limitRotationHead = cabezaDelAgente.localEulerAngles;

                                    if (limitRotationHead.x > 90f && limitRotationHead.x < 180f)
                                    {
                                        limitRotationHead.x = 90f;
                                    }
                                    if (limitRotationHead.x < 270f && limitRotationHead.x > 180f)
                                    {
                                        limitRotationHead.x = 270f;
                                    }

                                    if (limitRotationHead.y > 90f && limitRotationHead.y < 180f)
                                    {
                                        limitRotationHead.y = 90f;
                                    }
                                    if (limitRotationHead.y < 270f && limitRotationHead.y > 180f)
                                    {
                                        limitRotationHead.y = 270f;
                                    }

                                    if (limitRotationHead.z > 90f && limitRotationHead.z < 180f)
                                    {
                                        limitRotationHead.z = 90f;
                                    }
                                    if (limitRotationHead.z < 270f && limitRotationHead.z > 180f)
                                    {
                                        limitRotationHead.z = 270f;
                                    }

                                    cabezaDelAgente.localEulerAngles = limitRotationHead;

                                    break;
                                }
                            }
                            if(hitInfo.collider.CompareTag("Interactuable"))
                            {
                                interactuablePosition = hitInfo.transform;
                                

                                if(estaMirandoAlObjetivo == false)
                                {
                                    estaMirandoAlPlayer = false;
                                    estaMirandoAlObjetivo = true;
                                    agente.SetDestination(interactuablePosition.position);
                                    StartCoroutine(DeteccionDeInteractuable());
                                    agente.speed = velocidadAlCorrer;
                                }
                            }

                        }
                    }
                }
                break;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        if (detectionMode == DetectionMode.OverlapSphere)
        {
            Gizmos.DrawWireSphere(positionReference.transform.position, areaDetection);
        }
        else if (detectionMode == DetectionMode.VisionCone)
        {
            Gizmos.color = Color.yellow;
            Vector3 raycastPosition = positionReference.transform.position;

            Gizmos.DrawWireSphere(raycastPosition, maxDistance);

            Vector3 leftBoundary = Quaternion.Euler(0, -fieldOfView / 2, 0) * cabezaDelAgente.forward * maxDistance;
            Vector3 rightBoundary = Quaternion.Euler(0, fieldOfView / 2, 0) * cabezaDelAgente.forward * maxDistance;

            Gizmos.DrawLine(raycastPosition, raycastPosition + leftBoundary);
            Gizmos.DrawLine(raycastPosition, raycastPosition + rightBoundary);

            int segments = 20;
            for (int i = 0; i < segments; i++)
            {
                float angleStep = fieldOfView / segments;
                Vector3 segmentStart = Quaternion.Euler(0, -fieldOfView / 2 + i * angleStep, 0) * cabezaDelAgente.forward * maxDistance;
                Vector3 segmentEnd = Quaternion.Euler(0, -fieldOfView / 2 + (i + 1) * angleStep, 0) * cabezaDelAgente.forward * maxDistance;

                Gizmos.DrawLine(raycastPosition + segmentStart, raycastPosition + segmentEnd);
            }
        }
    }

    private void Start()
    {
        if(destinos.Count > 0)
        {
            agente.SetDestination(destinos[destinoActual].position);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Destino"))
        {
            destinoActual = (destinoActual + 1) % destinos.Count;

            StartCoroutine(TiempoDeEspera());
        }

        if(other.gameObject.CompareTag("Interactuable"))
        {
            tocandoInteractuable.Invoke();
        }
    }


    IEnumerator TiempoDeEspera()  //Cuanto tiempo pasará para que el agente viaje a un nuevo destino luego de haber llegado al primer destino.
    {
        yield return new WaitForSeconds(tiempoDeEsperaPorPunto);
        agente.SetDestination(destinos[destinoActual].position);
    }

    IEnumerator DeteccionDeInteractuable()
    {
        agente.isStopped = true;
        animSprite1.SetTrigger("ActivarSignoPregunta");
        yield return new WaitForSeconds(tiempoDeReaccionPorInteractuable);
        agente.isStopped = false;
        animSprite2.SetBool("ActivarSignoExclamacion", true);
    }
}
