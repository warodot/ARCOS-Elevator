using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Helper : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private NavMeshAgent agent;
    private Coroutine currentState;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float reachDist = 0.1f;
    private float currentSpeed;
    public Vector3 currentTarget;

    [Header("Interact")]
    [SerializeField] private float interactRange;
    [SerializeField] private LayerMask interactMask;
    private Interactable interactable;

    private void Start()
    {

    }
  
  

    public void SetMove(Vector3 target)
    {
        agent.SetDestination(target);
        //switchstate(movestate());
    }


    public void SetInteractable(Interactable _interactable)
    {
        interactable = _interactable;

    }
    

}
