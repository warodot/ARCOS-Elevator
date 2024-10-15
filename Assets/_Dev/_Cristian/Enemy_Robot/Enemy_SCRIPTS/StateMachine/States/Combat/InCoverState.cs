using ECM2;
using System.Collections;
using UnityEngine;

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
        _SM.StartCoroutine(_SM.CheckPlayerFlank());
    }

    public override void UpdateLogic()
    {
        _SM.CheckPlayerDistance();
        WaitForAttack();
        _SM.Turn();
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
        _SM.StopCoroutine(_SM.CheckPlayerFlank());
    }
}