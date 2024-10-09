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
        Attack();
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

    void Attack()
    {
        _SM.timeToAttack -= Time.deltaTime;
        if (_SM.attackCycle >= _SM.maxAttackCycle)
        {
            _SM.ChangeState(_SM.inCoverState);
        }
        if (_SM.currentAmmo == 0)
        {
            _SM.ChangeState(_SM.reloadingState);
        }
        if (_SM.timeToAttack < 0)
        {
            _SM.anim.SetTrigger("Attacking");
            FireRaycast();
            _SM.weaponSource.PlayOneShot(_SM.firingSFX);
            _SM.timeToAttack = _SM.timeToAttackMaster;
            _SM.currentAmmo--;
            _SM.attackCycle++;
        }
    }


    void FireRaycast()
    {
        if (Physics.Raycast(_SM.raycastSpawnPos.position, _SM.transform.forward, out RaycastHit hit, 200f))
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("PlayerHit");
            }
        }
    }

    public override void Exit()
    {
        _SM.timeToAttack = _SM.timeToAttackMaster;
        _SM.attackCycle = 0;
    }
}