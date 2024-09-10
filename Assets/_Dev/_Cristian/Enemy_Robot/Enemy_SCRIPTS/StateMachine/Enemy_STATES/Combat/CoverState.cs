using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoverState : BaseState
{
    private EnemySM _SM;

    public CoverState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        _SM.agent.enabled = false;
        _SM.agentObstacle.enabled = true;
        _SM.anim.SetBool("InCover", true);
        _SM.currentRotation = _SM.transform.rotation;
    }


    public override void UpdateLogic()
    {
        CombatManager();
    }


    public override void Exit()
    {
        _SM.agentObstacle.enabled = false;
        _SM.agent.enabled = true;
        _SM.anim.SetBool("InCover", false);

    }


    void CombatManager()
    {
        _SM.timeToAttackCurrent -= Time.deltaTime;

        if (_SM.timeToAttackCurrent < 0 && _SM.combatState != EnemySM.CombatState.Fighting)
        {
            _SM.StartCoroutine(Attack());
        }

        if (_SM.currentAmmo == 0)
        {
            Reload();
        }
    }

    IEnumerator Attack()
    {
        _SM.combatState = EnemySM.CombatState.Fighting;
        float attackCycles = 0;

        while (attackCycles < 3)
        {
            yield return new WaitForSeconds(1f);
            attackCycles++;
            _SM.currentAmmo--;
        }
        _SM.combatState = EnemySM.CombatState.Idling;
        yield break;
    }


    void Reload()
    {
        _SM.combatState = EnemySM.CombatState.Reloading;
        _SM.anim.SetTrigger("Reloading");
        Debug.Log("Reloaded");
        _SM.combatState = EnemySM.CombatState.Idling;
    }

}
