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
        float distance = 0;
        Transform selectedCoverSlot = null;

        List<CoverSpot> list = CoverSpotManager.instance.GetCoverSpots();
        for (int i = 0; i < list.Count; i++)
        {
            if(list[i].GetComponent<CoverSpot>().GetCanSeePlayer() == false && list[i].GetComponent<CoverSpot>().GetIsPicked() == false)
            {
                selectedCoverSlot = list[i].transform;
                selectedCoverSlot.GetComponent<CoverSpot>().SetIsPicked(true);
                break;
            }
        }

        for (int i = 0; i < list.Count; i++)
        {
            if (Vector3.Distance(_SM.transform.position, list[i].transform.position) < distance && !list[i].GetComponent<CoverSpot>().GetIsPicked() && !list[i].GetComponent<CoverSpot>().GetCanSeePlayer())
            {
                selectedCoverSlot.GetComponent <CoverSpot>().SetIsPicked(false);
                selectedCoverSlot = list[i].transform;
                selectedCoverSlot.GetComponent<CoverSpot>().SetIsPicked(true);
            }
        }
        _SM.agent.SetDestination(selectedCoverSlot.position);
        _SM.ChangeState(_SM.movingState);
    }
}
