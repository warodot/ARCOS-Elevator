using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Helper : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private NavMeshAgent agent;

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
