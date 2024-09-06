using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekCover : BaseState
{
    private EnemySM _SM;

    public SeekCover(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Entered");
        float distance = Vector3.Distance(_SM.transform.position, CoverSpotManager.instance.GetTransforms().ElementAt(0).transform.position);
        List<CoverSpot> list = CoverSpotManager.instance.GetTransforms();
        Transform selectedCoverSlot = CoverSpotManager.instance.GetTransforms().ElementAt(0).transform;

        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(_SM.transform.position, list[i].transform.position) < distance)
            {
                selectedCoverSlot = list[i].transform;
            }
        }
        _SM.agent.SetDestination(selectedCoverSlot.position);
        _SM.ChangeState(_SM.movingState);
    }
}
