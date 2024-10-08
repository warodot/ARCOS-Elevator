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
        _SM.turnRate = 280f;
        _SM.enemyState = EnemySM.EnemyState.Idle;
        _SM.switchToAttackTime = Random.Range(2f,6f);
    }

    public override void UpdateLogic()
    {
        WaitForAttack();
        Turn();
    }

    void WaitForAttack()
    {
        _SM.switchToAttackTime -= Time.deltaTime;
        if(_SM.switchToAttackTime <= 0)
        {
            _SM.ChangeState(_SM.shootingState);
        }
    }
    void Turn()
    {
        var towardsPlayer = MapPlayerPosManager.instance.GetPlayerRef().transform.position - _SM.transform.position;
        towardsPlayer.y = 0;
        _SM.transform.rotation = Quaternion.RotateTowards(
            from: _SM.transform.rotation,
            to: Quaternion.LookRotation(towardsPlayer),
            maxDegreesDelta: Time.deltaTime * _SM.turnRate
        );
    }
}