using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToCoverState : BaseState
{
    EnemySM _SM;

    public MoveToCoverState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }
}
