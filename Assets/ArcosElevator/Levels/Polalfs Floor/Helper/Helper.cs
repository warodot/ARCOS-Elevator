using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Helper : MonoBehaviour
{
    private Animator animator;
    private CharacterController controller;
    private Coroutine currentState;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float reachDist = 0.1f;
    private float currentSpeed;
    private Vector3 currentTarget;

    [Header("Interact")]
    [SerializeField] private float interactRange;
    [SerializeField] private LayerMask interactMask;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        SwitchState(IdleState());
    }
    private void SwitchState(IEnumerator state)
    {
        if(currentState != null) StopCoroutine(currentState);

        if (state == null) return;

        currentState = StartCoroutine(state);
    }

    IEnumerator IdleState()
    {
        currentSpeed = 0;   
        while(true)
        {
            //if (currentTarget != null) SwitchState(MoveState());
            
            yield return null;
        }
    }
    
    public void SetMove(Vector3 target)
    {
        currentTarget = target;
        SwitchState(MoveState());
    }
    IEnumerator MoveState()
    {
        currentSpeed = speed;
        float dirDis = Mathf.Atan2(currentTarget.z - transform.position.z, currentTarget.x - transform.position.x);
        Vector3 dir = new Vector3(Mathf.Cos(dirDis), transform.position.y, Mathf.Sin(dirDis));

        while (true)
        {
            controller.Move(dir.normalized * currentSpeed);
            float dist = Vector3.Distance(transform.position, currentTarget);
            if (dist <= reachDist) SwitchState(IdleState());
            yield return null;
        }
    }

}
