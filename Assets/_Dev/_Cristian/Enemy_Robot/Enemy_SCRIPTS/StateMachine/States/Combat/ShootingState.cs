using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShootingState : BaseState
{
    EnemySM _SM;

    public ShootingState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        SetMaxAttackCycle();
        _SM.turnRate = 90f;
        _SM.enemyState = EnemySM.EnemyState.Attacking;
    }

    public override void UpdateLogic()
    {
        _SM.Attack();
        _SM.Turn();
    }


    void SetMaxAttackCycle()
    {
        _SM.maxAttackCycle = _SM.soldierClass switch
        {
            EnemySM.SoldierClass.Rifleman => Random.Range(3, 6),
            EnemySM.SoldierClass.MachineGunner => Random.Range(6, 10),
            EnemySM.SoldierClass.Submachinegunner => Random.Range(2, 4),
            _ => (float)3,
        };
    }

    public override void Exit()
    {
        _SM.timeToAttack = _SM.timeToAttackMaster;
        _SM.attackCycle = 0;
        _SM.muzzleFlash.SetActive(false);
    }
}