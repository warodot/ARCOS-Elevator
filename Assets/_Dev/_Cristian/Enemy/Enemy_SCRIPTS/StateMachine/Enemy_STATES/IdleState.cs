using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : BaseState
{
    private EnemySM _SM;

    public IdleState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
    }


    public override void UpdateLogic()
    {
        if (_SM.agent.remainingDistance < 0.2f)
        {
            _SM.GetRandomPoint(_SM.transform.position, _SM.movementRadius, out Vector3 point);
            _SM.agent.SetDestination(point);
        }
    }
}
