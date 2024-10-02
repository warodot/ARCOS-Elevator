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
    private InteractableObject interactable;

    [Header("Grab")]
    [SerializeField] private GameObject box;
    [SerializeField] private Transform dropSpawn;
    private Vector3 _dropPoint;
    private bool _drop;
    private void Update()
    {
        if(interactable != null)
        {
            float dist = Vector3.Distance(transform.position, interactable.transform.position);
            if (dist < interactRange)
            {
                transform.LookAt(new Vector3(interactable.transform.position.x,transform.position.y, interactable.transform.position.z));
                StartCoroutine(Interactuar());
               
            }
        }
        if(_drop)
        {
            float distDrop = Vector3.Distance(transform.position, _dropPoint);
            if (distDrop < interactRange)
            {
                Drop();
            }
        }
    }


    public void SetMove(Vector3 target)
    {
        target = new Vector3(target.x, transform.position.y, target.z);
        agent.SetDestination(target);
    }


    public void SetInteractable(InteractableObject _interactable)
    {
        interactable = _interactable;
    }

    public void SetDrop(Vector3 dropTarget)
    {
        _drop = true;
        _dropPoint = dropTarget;
    }

    private void Drop()
    {
        agent.isStopped = true;

        box.SetActive(true);
        box.transform.position = dropSpawn.position;
    }
    private IEnumerator Interactuar()
    {
        yield return new WaitForSeconds(1f);
        if(interactable.ShowType() == TypeOfInteract.Grab)
        {
            box = interactable.gameObject;
            box.gameObject.SetActive(false);
            interactable = null;
        }
        else if(interactable.ShowType() == TypeOfInteract.Input)
        {
            Debug.Log("Lo presiono");
        }
    }
    
    

}
