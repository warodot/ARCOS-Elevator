using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingState : BaseState
{
    private EnemySM _SM;

    public MovingState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        _SM.agent.speed = _SM.runningSpeed;
    }

    public override void UpdateLogic()
    {
        UpdateAnimation();
        CheckDistanceLeft();
    }

    void UpdateAnimation()
    {
        float speed = Mathf.Min(1, _SM.GetAgentSpeed());
        _SM.anim.SetFloat("currentSpeed", speed);
    }

    void CheckDistanceLeft()
    {
        if(_SM.agent.remainingDistance < 0.2f)
        {
            _SM.ChangeState(_SM.inCoverState);
        }
    }
}
