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
        SuppresiveFire();
        _SM.Turn();
    }

    void SuppresiveFire()
    {
        _SM.timeToAttack -= Time.deltaTime;

        if (_SM.currentAmmo == 0)
        {
            _SM.ChangeState(_SM.reloadingState);
        }
        if (_SM.timeToAttack < 0)
        {
            _SM.anim.SetTrigger("Attacking");
            _SM.FireRaycast();
            _SM.weaponSource.PlayOneShot(_SM.firingSFX);
            _SM.timeToAttack = _SM.timeToAttackMaster;
            _SM.currentAmmo--;
        }
    }


}
