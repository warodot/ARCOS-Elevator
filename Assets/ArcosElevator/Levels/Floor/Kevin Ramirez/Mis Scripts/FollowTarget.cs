using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.AI;
using UnityEngine.AI;

public class FollowTarget : MonoBehaviour
{
    public Animator anim;
    public NavMeshAgent navmesh;
    public Transform player;


    // Update is called once per frame
    void Update()
    {
        navmesh.SetDestination(player.position);

        if(navmesh.velocity.magnitude > 0)
        {
            anim.SetBool("Caminar", true);
        }
        else
        {
            anim.SetBool("Caminar", false);
        }
    }
}
