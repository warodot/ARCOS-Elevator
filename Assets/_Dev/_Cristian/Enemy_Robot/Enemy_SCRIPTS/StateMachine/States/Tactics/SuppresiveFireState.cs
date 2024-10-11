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
        if (_SM.currentAmmo > 10)
        {
            //does nothing;
        }
        else
        {
            _SM.anim.SetTrigger("Reloading");
            _SM.StartCoroutine(WaitForAnimation());
        }
        _SM.maxAttackCycle = _SM.currentAmmo;
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(7f);
        _SM.currentAmmo = _SM.maxAmmo;
        _SM.ChangeState(_SM.inCoverState);
    }

    public override void UpdateLogic()
    {
        _SM.SuppresiveFire();
        _SM.Turn();
    }
}