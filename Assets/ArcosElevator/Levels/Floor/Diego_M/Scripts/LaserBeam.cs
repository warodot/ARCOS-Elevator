using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public float speed = 10f;
    public float lifespan = 5f;

    private Vector3 direction;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = transform.forward;
        rb.velocity = direction * speed;
        Destroy(gameObject, lifespan);
    }
    private void FixedUpdate()
    {
        if (rb.velocity != Vector3.zero)
        {
            Quaternion rotation = Quaternion.LookRotation(rb.velocity.normalized);
            rb.rotation = rotation;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        direction = Vector3.Reflect(direction, collision.contacts[0].normal);
        rb.velocity = direction * speed;
    }
}
