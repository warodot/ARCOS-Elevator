using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;
using Unity.VisualScripting;

public class SeekCoverState : BaseState
{
    EnemySM _SM;

    public SeekCoverState(EnemySM stateMachine) : base(stateMachine)
    {
        _SM = stateMachine;
    }

    public override void Enter()
    {
        for (int i = 0; i < _SM.seekingIterations; i++)
        {
            Debug.Log("Cycle " + i);
            Vector3 spawnPoint = _SM.transform.position;
            Vector2 offset = Random.insideUnitCircle * i;
            spawnPoint.x += offset.x;
            spawnPoint.z += offset.y;
            NavMesh.FindClosestEdge(spawnPoint, out NavMeshHit hit, NavMesh.AllAreas);
            _SM.storedHits.Add(hit);
        }

        for (int i = 0; i < _SM.storedHits.Count; i++)
        {
                Debug.Log(_SM.storedHits[i].mask);
            if (_SM.storedHits[i].mask != NavMesh.GetAreaFromName("Cover"))
            {
                _SM.storedHits.RemoveAt(i);
            }
        }

        var sortedList = _SM.storedHits.OrderBy(x => x.distance);

        foreach (NavMeshHit hit in sortedList)
        {
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