using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

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
        _SM.RebakeNavmesh();
    }

    void GetFlankPosition()
    {
        for (int i = 0; i < _SM.seekingIterations; i++)
        {
            Vector3 spawnPoint = _SM.transform.position;
            Vector2 offset = Random.insideUnitCircle * i;
            spawnPoint.x += offset.x;
            spawnPoint.z += offset.y;
            NavMesh.FindClosestEdge(spawnPoint, out NavMeshHit hit, NavMesh.AllAreas);
            _SM.storedHits.Add(hit);
        }

        for (int i = 0; i < _SM.storedHits.Count; i++)
        {
            if (_SM.storedHits[i].mask != 16)
            {
                _SM.storedHits.RemoveAt(i);
            }
        }

        var sortedList = _SM.storedHits.OrderBy(x => x.distance);

        foreach (NavMeshHit hit in sortedList)
        {
            Debug.Log(Vector3.Dot(hit.normal, MapPlayerPosManager.instance.GetPlayerRef().transform.position - _SM.transform.position));
            if (Vector3.Dot(hit.normal, MapPlayerPosManager.instance.GetPlayerRef().transform.position - _SM.transform.position) < 0
                && Physics.Linecast(hit.position, MapPlayerPosManager.instance.GetPlayerRef().transform.position, _SM.whatIsCover))
            {
                _SM.agent.SetDestination(hit.position);
                _SM.ChangeState(_SM.moveToCoverState);
                break;
            }
        }
    }
}
