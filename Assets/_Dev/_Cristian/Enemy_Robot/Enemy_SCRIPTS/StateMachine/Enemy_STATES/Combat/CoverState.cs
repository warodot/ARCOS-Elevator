using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
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
        if(_SM.combatState != EnemySM.CombatState.Reloading)
        { 
            _SM.timeToAttackCurrent -= Time.deltaTime;
        }

        if (_SM.timeToAttackCurrent < 0 && _SM.combatState == EnemySM.CombatState.Idling)
        {
            _SM.StartCoroutine(Attack());
        }

        if(_SM.timeToAttackCurrent < 0)
        {
            RotateTowardsPlayer();
        }

        if (_SM.currentAmmo <= 0 && _SM.combatState != EnemySM.CombatState.Reloading)
        {
            _SM.StopCoroutine(Attack());
            _SM.StartCoroutine(Reload());
        }
    }

    IEnumerator Attack()
    {
        _SM.combatState = EnemySM.CombatState.Fighting;
        
        float attackCycles = 0;
        while (attackCycles < _SM.maxAttackCycles)
        {
            yield return new WaitForSeconds(_SM.timeBetweenShots);
            _SM.anim.SetTrigger("Attacking");
            _SM.gunAudioSource.PlayOneShot(_SM.gunshotClip);
            _SM.flashSFX.SetActive(true);
            attackCycles++;
            _SM.currentAmmo--;
        }
        _SM.combatState = EnemySM.CombatState.Idling;
        _SM.timeToAttackCurrent = Random.Range(1,4);
        yield break;
    }

    void RotateTowardsPlayer()
    {
        _SM.transform.LookAt(MapPlayerPosManager.instance.GetPlayerRef().transform.position);
    }

    IEnumerator Reload()
    {
        _SM.combatState = EnemySM.CombatState.Reloading;
        _SM.anim.SetTrigger("Reloading");
        yield return new WaitForSeconds(6.87f);
        _SM.currentAmmo = _SM.maxAmmo;
        _SM.combatState = EnemySM.CombatState.Idling;
    }

}
