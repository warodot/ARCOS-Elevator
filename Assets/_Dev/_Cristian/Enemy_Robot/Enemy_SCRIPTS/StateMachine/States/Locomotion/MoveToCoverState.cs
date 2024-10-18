using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class MoveToCoverState : BaseState
{
    EnemySM _SM;

    public MoveToCoverState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        _SM.enemyState = EnemySM.EnemyState.Moving;
        _SM.anim.SetBool("InCover", false);
        _SM.maxAttackCycle = Random.Range(2, 5);
        _SM.turnRate = 360;
    }

    public override void UpdateLogic()
    {
        MoveForward();
    }

    void MoveForward()
    {
        _SM.anim.SetFloat("currentSpeed", 1);
        if (_SM.agent.remainingDistance <= 0.02f)
        {
            _SM.ChangeState(_SM.inCoverState);
        }
    }
}
