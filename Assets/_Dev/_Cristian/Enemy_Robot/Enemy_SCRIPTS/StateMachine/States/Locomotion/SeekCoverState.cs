using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class SeekCoverState : BaseState
{
    EnemySM _SM;

    public SeekCoverState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void UpdateLogic()
    {
       _SM.FindClosestEdge();
    }
}
