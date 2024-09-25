using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UIElements;

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
        if(_SM.canAttack)
        { 
            CombatManager();
        }
        else _SM.combatState = EnemySM.CombatState.Idling;
    }

    public override void UpdatePhysics()
    {
        if(_SM.combatState != EnemySM.CombatState.Reloading)
        {
            RotateTowardsPlayer();
        }
    }

    public override void Exit()
    {
        _SM.agentObstacle.enabled = false;
        _SM.agent.enabled = true;
        _SM.anim.SetBool("InCover", false);

    }


    void CombatManager()
    {
        if (_SM.combatState != EnemySM.CombatState.Reloading)
        {
            _SM.timeToAttackCurrent -= Time.deltaTime;
        }

        if (_SM.timeToAttackCurrent < 0 && _SM.combatState == EnemySM.CombatState.Idling)
        {
            _SM.StartCoroutine(Attack());
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
            CastRay();
            _SM.flashSFX.SetActive(true);
            attackCycles++;
            _SM.currentAmmo--;
        }
        _SM.combatState = EnemySM.CombatState.Idling;
        _SM.timeToAttackCurrent = Random.Range(1, 4);
        yield break;
    }


    void RotateTowardsPlayer()
    {
        Vector3 direction = MapPlayerPosManager.instance.GetPlayerRef().transform.position - _SM.transform.position;
        Quaternion toRotation = Quaternion.LookRotation(direction, _SM.transform.up);
        toRotation.x = 0;
        toRotation.z = 0;
        _SM.transform.rotation = Quaternion.Lerp(_SM.transform.rotation, toRotation, _SM.rotationSpeed * Time.deltaTime);
    }

    void CastRay()
    {
        Vector3 newPos = new(_SM.transform.position.x, _SM.transform.position.y + 1.5f, _SM.transform.position.z);
        if (Physics.Raycast(newPos, _SM.transform.forward, out RaycastHit hit, Mathf.Infinity))
        {
            switch (hit.collider.tag)
            {
                case "Player":
                    hit.collider.GetComponent<Health>().SetHealth(hit.collider.GetComponent<Health>().GetHealth() - _SM.weaponDamage);
                    Debug.Log("Hit");
                    Debug.DrawRay(newPos, _SM.transform.forward);
                    break;

                default:
                    Debug.Log("Didnt Hit Player");
                    break;
            }
        }
        else
        {
            Debug.Log("No Hit");
            return;
        }
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
