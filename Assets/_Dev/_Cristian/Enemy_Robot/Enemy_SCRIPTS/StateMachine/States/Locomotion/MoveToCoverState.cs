using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class MoveToCoverState : BaseState
{
    EnemySM _SM;

    public MoveToCoverState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        _SM.enemyState = EnemySM.EnemyState.Moving;
        _SM.anim.SetBool("InCover", false);
        _SM.maxAttackCycle = Random.Range(2, 5);
    }

    public override void UpdateLogic()
    {
        MoveForward();
    }

    void MoveForward()
    {
        _SM.anim.SetFloat("currentSpeed", _SM.agent.velocity.magnitude);

        if (_SM.agent.remainingDistance <= 0.02f)
        {
            _SM.ChangeState(_SM.inCoverState);
        }
    }
    /*
    void MoveBackwards()
    {
        if (_SM.isWalkingBackwards == true) _SM.agent.updateRotation = false;
        _SM.anim.SetFloat("currentSpeed", _SM.agent.velocity.magnitude * -1);
        _SM.Turn();
        FireOnTheMove();
        if (_SM.agent.remainingDistance <= 0.1f)
        {
            _SM.isWalkingBackwards = false;
            _SM.ChangeState(_SM.inCoverState);
        }
        else if (Vector3.Distance(_SM.transform.position, MapPlayerPosManager.instance.GetPlayerRef().transform.position) <= 5f)
        {
            _SM.isWalkingBackwards = false;
        }
    }

    void FireOnTheMove()
    {
        _SM.timeToAttack -= Time.deltaTime;
        if (_SM.timeToAttack < 0 && _SM.currentAmmo > 0 && _SM.attackCycle < _SM.maxAttackCycle)
        {
            _SM.anim.SetTrigger("Attacking");
            _SM.FireRaycast();
            _SM.weaponSource.PlayOneShot(_SM.firingSFX);
            _SM.timeToAttack = _SM.timeToAttackMaster;
            _SM.currentAmmo--;
            _SM.attackCycle++;
        }
    }
    */
    public override void Exit()
    {
        Debug.Log("fuck");
        _SM.anim.SetBool("InCover", true);
    }
}
