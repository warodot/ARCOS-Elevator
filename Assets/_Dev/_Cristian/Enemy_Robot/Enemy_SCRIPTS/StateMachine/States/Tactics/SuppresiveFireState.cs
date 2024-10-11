using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppresiveFireState : BaseState
{
    EnemySM _SM;

    public SuppresiveFireState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        _SM.turnRate = 90f;
        _SM.enemyState = EnemySM.EnemyState.Attacking;
        _SM.maxAttackCycle = _SM.currentAmmo;
    }

    public override void UpdateLogic()
    {
        _SM.SuppresiveFire();
        _SM.Turn();
    }
}