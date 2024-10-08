using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReloadingState : BaseState
{
    EnemySM _SM;

    public ReloadingState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        _SM.enemyState = EnemySM.EnemyState.Reloading;
        _SM.anim.SetTrigger("Reloading");
        _SM.StartCoroutine(WaitForAnimation());
    }

    IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(7f);
        _SM.currentAmmo = _SM.maxAmmo;
        _SM.ChangeState(_SM.inCoverState);
    }
}