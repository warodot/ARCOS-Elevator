using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Unity.VisualScripting;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class SeekCoverState : BaseState
{
    EnemySM _SM;

    public SeekCoverState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        _SM.agent.enabled = true;
        _SM.obstacle.enabled = false;
        SeekClosestCover();
    }

    void SeekClosestCover()
    {
        for (int i = 0; i < _SM.seekingIterations; i++)
        {
            Vector3 spawnPoint = MapPlayerPosManager.instance.GetCoverRef().position;
            Vector2 offset = Random.insideUnitCircle * i;
            spawnPoint.x += offset.x;
            spawnPoint.z += offset.y;
            NavMesh.FindClosestEdge(spawnPoint, out NavMeshHit hit, NavMesh.AllAreas);
            Debug.Log(hit.position);
            _SM.storedHits.Add(hit);
            Debug.Log(_SM.storedHits.Count);
        }
        var sortedList = _SM.storedHits.OrderBy((d) => (d.position - _SM.transform.position).sqrMagnitude).ToArray();

        foreach (NavMeshHit hit in sortedList)
        {
            if (Vector3.Dot(
                lhs: hit.normal,
                rhs: MapPlayerPosManager.instance.GetPlayerRef().transform.position - _SM.transform.position) < 0
                &&
                Physics.Linecast(
                    start: hit.position,
                    end: MapPlayerPosManager.instance.GetPlayerRef().transform.position,
                    layerMask: _SM.whatIsCover))
            {
                _SM.agent.SetDestination(hit.position);
                _SM.ChangeState(_SM.moveToCoverState);
                break;
            }
        }
    }

    public override void Exit()
    {
        _SM.anim.SetBool("InCover", false);
    }
}