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
    }

    public override void UpdateLogic()
    {

        Turn();
    }

    void Attack()
    {
        _SM.timeToAttack -= Time.deltaTime;
        if(_SM.timeToAttack < 0)
        {
            _SM.anim.SetTrigger("Attacking");
            _SM.timeToAttack = 0.1f;
        }
    }

    void Turn()
    {
        var towardsPlayer = MapPlayerPosManager.instance.GetPlayerRef().transform.position - _SM.transform.position;

        _SM.transform.rotation = Quaternion.RotateTowards(
            _SM.transform.rotation,
            Quaternion.LookRotation(towardsPlayer),
            Time.deltaTime * _SM.turnRate
        );
    }
}