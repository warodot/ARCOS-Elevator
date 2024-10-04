using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCoverState : BaseState
{
    EnemySM _SM;

    public InCoverState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        _SM.turnRate = 280f;
        _SM.enemyState = EnemySM.EnemyState.Idle;
    }

    public override void UpdateLogic()
    {
        var towardsPlayer = MapPlayerPosManager.instance.GetPlayerRef().transform.position - _SM.transform.position;

        _SM.transform.rotation = Quaternion.RotateTowards(
            _SM.transform.rotation,
            Quaternion.LookRotation(towardsPlayer),
            Time.deltaTime * _SM.turnRate
        );
    }
}