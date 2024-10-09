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

    void WaitForAttackSet()
    {
        _SM.switchToAttackTime = _SM.mentalState switch
        {
            EnemySM.MentalState.Fresh => Random.Range(2f, 3f),
            EnemySM.MentalState.Concerned => Random.Range(4f, 7f),
            EnemySM.MentalState.Scared => Random.Range(7f, 10f),
            _ => (float)2f,
        };
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