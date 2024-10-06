using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RageMode : MonoBehaviour
{
    public Transform playerTransform;
    public Transform screamerPosition;
    public bool rageMode;
    public NavMeshAgent agent;
    public ControllerIA controllerIAScript;

    public bool screamerActivado;



    void Update()
    {
        if (rageMode == true)
        {
            StartCoroutine(Despertar());
        }

        if(screamerActivado)
        {
            controllerIAScript.agentAnim.SetBool("ScreamerDeath", true);
            this.gameObject.transform.position = Vector3.Lerp(transform.position, screamerPosition.position, 1);

            Quaternion rotacionCuerpoObjetivo = Quaternion.LookRotation(controllerIAScript.mirarObjetivo.position - controllerIAScript.cabezaDelAgente.position);
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotacionCuerpoObjetivo, 1);
            controllerIAScript.agente.enabled = false;
            controllerIAScript.enabled = false;
        }
    }

    IEnumerator Despertar()
    {
        controllerIAScript.agentAnim.SetBool("Despertar", true);
        controllerIAScript.muerteDePersonaje = false;
        agent.isStopped = true;
        yield return new WaitForSeconds(4.2f);
        agent.isStopped = false;
        controllerIAScript.raycastActivado = false;
        rageMode = true;
        agent.SetDestination(playerTransform.position);
        controllerIAScript.agentAnim.SetBool("RunRageMode", true);
        controllerIAScript.interactableGameObjectTag.SetActive(false);
        controllerIAScript.enabled = false;
        agent.speed = 6;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (rageMode)
            {
                screamerActivado = true;
            }
        }
    }

}
