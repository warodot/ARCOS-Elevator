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
        _SM.turnRate = 180f;
        _SM.enemyState = EnemySM.EnemyState.Attacking;
    }

    public override void UpdateLogic()
    {
        Attack();
        Turn();
    }

    void Attack()
    {
        _SM.timeToAttack -= Time.deltaTime;
        if(_SM.attackCycle >= _SM.maxAttackCycle)
        {
            _SM.ChangeState(_SM.inCoverState);
        }
        if(_SM.timeToAttack < 0)
        {
            _SM.anim.SetTrigger("Attacking");
            _SM.timeToAttack = _SM.timeToAttackMaster;
            _SM.attackCycle++;  
        }

    }

    void Turn()
    {
        var towardsPlayer = MapPlayerPosManager.instance.GetPlayerRef().transform.position - _SM.transform.position;
        towardsPlayer.y = 0;
        _SM.transform.rotation = Quaternion.RotateTowards(
            from:_SM.transform.rotation,
            to:Quaternion.LookRotation(towardsPlayer),
            maxDegreesDelta:Time.deltaTime * _SM.turnRate
        );
    }

    public override void Exit()
    {
        _SM.timeToAttack = _SM.timeToAttackMaster;
        _SM.attackCycle = 0;
    }
}