using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public LayerMask bounceLayer;
    public ParticleSystem bounceFX;
    public ParticleSystem collisionFX;
    public float speed = 10f;
    private int maxBounces = 5;
    private int bounceCount = 0;   
    private Vector3 direction;
    private Rigidbody rb;
    private AudioSource audioSource;
    public AudioClip laserImpact;
    public GameObject laserBody;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        direction = transform.forward;
        rb.velocity = direction * speed;
        bounceCount = 0;
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
            }
            else
            {
                StartCoroutine(LaserCollision());
            }
        }
        else
        {
            StartCoroutine(LaserCollision());
        }
    }
    IEnumerator LaserCollision()
    {
        rb.isKinematic = true;
        collisionFX.Play();
        laserBody.SetActive(false);
        audioSource.PlayOneShot(laserImpact);
        
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
