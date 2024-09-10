using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }
}
