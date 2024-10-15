using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MoverNoVer : MonoBehaviour
{
     public Transform player; 
    public float moveSpeed = 2.0f; 
    public float stopDistance = 2.0f; 
    public float detectionRange = 20.0f; 
    private Rigidbody rb;

    public bool look;

    private NavMeshAgent agent;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Camera.current.name == "SceneCamera") return;
        if(look)
        {
             //MoveTowardsPlayer();
            agent.destination = player.position;
             
        }
    }

    public void OnInvisible()
    {
        look = true;
        agent.isStopped = false;
        Debug.Log("Fuera de camara");
        
    }
    
  public  void OnVisible()
    {
        look = false;
       agent.isStopped = true;
        Debug.Log("En Camara");
        
    }

    void MoveTowardsPlayer()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer > stopDistance && distanceToPlayer <= detectionRange)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            Vector3 moveForce = direction * moveSpeed * Time.deltaTime;
            rb.MovePosition(rb.position + moveForce); 
        }
    }
}
