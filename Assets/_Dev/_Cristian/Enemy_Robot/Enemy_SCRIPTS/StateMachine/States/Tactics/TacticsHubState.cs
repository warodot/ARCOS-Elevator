using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TacticsHubState : BaseState
{
    EnemySM _SM;

    public TacticsHubState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        switch (_SM.selectedTactic)
        {
            case 0: //Grenade Throw
                _SM.ChangeState(_SM.grenadeThrowState);
                break;

            case 1: //Suppresive Fire
                _SM.ChangeState(_SM.suppresiveFireState);
                break;            
                
            default:
                break;
        }
    }
}
