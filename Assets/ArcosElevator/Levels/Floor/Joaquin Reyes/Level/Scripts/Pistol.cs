using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Animator animator;
    public Vector3 lastPosition;
    public Transform player;
    public GameObject projectilePrefab;    
    public Transform shootPoint;  
    public float projectileSpeed = 20f;
    public AudioSource audio;
    public AudioClip charge;
    public AudioClip shoot;
    private float stepTimer;
    public float stepInterval = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        lastPosition = player.position;
        stepTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Shoot();

        if (Vector3.Distance(player.position, lastPosition) > 0.02f)
        {
          
            animator.SetBool("Walk", true);
        }
        else
        {
            
            animator.SetBool("Walk", false);
            
        }
        lastPosition = player.position;

        if (Input.GetMouseButton(0))
        {
            stepTimer -= Time.deltaTime;
            if (stepTimer <= 0)
            {

                audio.clip = charge;
                audio.Play();


                stepTimer = stepInterval;
            }
        }

    }

    void Shoot()
    {
        
        if (Input.GetMouseButton(0))
        {
            animator.SetBool("Charge", true);
            
        }
        else if (Input.GetMouseButtonUp(0))
        {
            animator.SetBool("Charge", false);

            animator.SetBool("Shoot", true);
            
            GameObject projectile = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);

            audio.clip = shoot;
            audio.Play();

            animator.SetBool("Shoot", false);

            
            Rigidbody rb = projectile.GetComponent<Rigidbody>();
            if (rb != null)
            {

                rb.velocity = shootPoint.forward * projectileSpeed;
            }
        }
        
    }
}

    
