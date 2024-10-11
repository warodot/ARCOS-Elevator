using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlankingState : BaseState
{
    EnemySM _SM;

    public FlankingState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        if(_SM.givenRole == 0)
        {
            _SM.SuppresiveFire();
        }
        else if(_SM.givenRole == 1)
        {
            Flank();
        }
    }


    void Flank()
    {

    }
}
