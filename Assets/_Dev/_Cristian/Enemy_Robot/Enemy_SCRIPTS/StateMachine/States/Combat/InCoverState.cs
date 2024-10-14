using ECM2;
using System.Collections;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class InCoverState : BaseState
{
    EnemySM _SM;

    public InCoverState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        EnemiesManager.instance.AddEnemy(_SM);
        _SM.turnRate = 280f;
        _SM.enemyState = EnemySM.EnemyState.InCover;
        WaitForAttackSet();
    }

    public override void UpdateLogic()
    {
        WaitForAttack();
        _SM.Turn();
    }

    void CheckPlayerDistance()
    {
        if (Vector3.Distance(_SM.transform.position, MapPlayerPosManager.instance.GetPlayerRef().transform.position) < 5f)
        {
            _SM.ChangeState(_SM.seekCoverState);
        }
    }


    void CheckPlayerFlank()
    {

    }
    void WaitForAttackSet()
    {
        _SM.switchToAttackTime = Random.Range(2f, 3f);
    }


    void WaitForAttack()
    {
        _SM.switchToAttackTime -= Time.deltaTime;
        if (_SM.switchToAttackTime <= 0)
        {
            _SM.ChangeState(_SM.shootingState);
        }
    }

    public override void Exit()
    {
        _SM.switchToAttackTime = 0;
        EnemiesManager.instance.RemoveEnemy(_SM);
    }
}