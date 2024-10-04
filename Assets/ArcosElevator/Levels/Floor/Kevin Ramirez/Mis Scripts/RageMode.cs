using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RageMode : MonoBehaviour
{
    public Transform playerTransform;
    public bool rageMode;
    public NavMeshAgent agent;
    public ControllerIA controllerIAScript;



    void Update()
    {
        if(rageMode == true)
        {
            StartCoroutine(Despertar());
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
}
