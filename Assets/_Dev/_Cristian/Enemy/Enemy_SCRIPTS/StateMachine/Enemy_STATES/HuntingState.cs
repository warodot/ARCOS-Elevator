using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HuntingState : BaseState
{
    private EnemySM _SM;

    public HuntingState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }
}
