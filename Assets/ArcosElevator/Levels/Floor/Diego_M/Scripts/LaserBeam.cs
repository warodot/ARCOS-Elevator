using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public LayerMask bounceLayer;
    public float speed = 10f;
    private int maxBounces = 2;
    private int bounceCount = 0;   
    private Vector3 direction;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        direction = transform.forward;
        rb.velocity = direction * speed;
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
        if (((1 << collision.gameObject.layer) & bounceLayer) != 0)
        {
            if (bounceCount < maxBounces)
            {
                direction = Vector3.Reflect(direction, collision.contacts[0].normal);
                rb.velocity = direction * speed;
                bounceCount++;
                //Debug.Break();
            }
            else
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
