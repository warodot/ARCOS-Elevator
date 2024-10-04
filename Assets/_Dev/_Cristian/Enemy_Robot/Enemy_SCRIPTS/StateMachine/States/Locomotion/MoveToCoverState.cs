using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MoveToCoverState : BaseState
{
    EnemySM _SM;

    public MoveToCoverState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void UpdateLogic()
    {
        _SM.anim.SetFloat("currentSpeed", _SM.agent.velocity.magnitude);

        if(_SM.agent.remainingDistance <= 0)
        {
            _SM.ChangeState(_SM.inCoverState);
        }
    }

    public override void Exit()
    {
        _SM.anim.SetBool("InCover", true);
    }
}
