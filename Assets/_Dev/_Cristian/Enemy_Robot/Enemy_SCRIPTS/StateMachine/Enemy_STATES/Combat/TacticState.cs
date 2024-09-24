using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TacticState : BaseState
{
    private EnemySM _SM;

    public TacticState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        
    }

    public override void UpdateLogic()
    {
        
    }

    public override void Exit()
    {
        
    }
}
