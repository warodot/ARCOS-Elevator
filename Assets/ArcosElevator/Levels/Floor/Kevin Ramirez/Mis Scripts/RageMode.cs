using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RageMode : MonoBehaviour
{
    public Transform playerTransform;
    public Transform screamerPosition;
    public Animator agentAnim2;
    public bool rageMode;
    public NavMeshAgent agent;
    public ControllerIA controllerIAScript;

    public bool screamerActivado;



    void Update()
    {
        if (rageMode!)
        {
            StartCoroutine(Despertar());
        }

        if(screamerActivado!)
        {
            controllerIAScript.agente.enabled = false;
            controllerIAScript.enabled = false;
            agentAnim2.SetBool("ScreamerDeath", true);

            this.gameObject.transform.parent = screamerPosition;
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            this.gameObject.transform.localEulerAngles = new Vector3(0, 180, 0);
            
        }
    }

    IEnumerator Despertar()
    {
        controllerIAScript.agentAnim.SetBool("Despertar", true);
        controllerIAScript.muerteDePersonaje = false;
        agent.isStopped = true;
        yield return new WaitForSeconds(4.2f);
        controllerIAScript.raycastActivado = false;
        rageMode = true;
        agent.SetDestination(playerTransform.position);
        agent.isStopped = false;
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

    public void RotacionPersonaje()
    {
        gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
    }
}
