using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingState : BaseState
{
    private EnemySM _SM;

    public RoamingState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }
}
