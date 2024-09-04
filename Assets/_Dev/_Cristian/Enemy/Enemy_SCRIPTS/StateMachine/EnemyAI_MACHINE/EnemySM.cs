using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySM : StateMachine
{
    [HideInInspector] RoamingState roamingState;
    [HideInInspector] IdleState idleState;
    [HideInInspector] HuntingState huntingState;


    public NavMeshAgent agent;

    //Enemy AI
    public Vector3 playerPos;
    public float movementRadius;

    private void Awake()
    {
        roamingState = new RoamingState(this);
        idleState = new IdleState(this);
        huntingState = new HuntingState(this);
    }

    protected override BaseState GetInitialState()
    {
        return idleState;
    }

    public bool GetRandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            if (NavMesh.SamplePosition(randomPoint, out NavMeshHit hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, movementRadius);
    }

}

