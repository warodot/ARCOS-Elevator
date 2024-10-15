using LucasRojo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundEnemy : MonoBehaviour
{
    public bool isSpecial = false;
    public bool isFlying = false;
    public bool fromLeft = true;
    public float speed = 2;
    [Space]
    public int Maxlife = 1;
    public int currentLife;
    [Space]
    public GameObject deathParticle;
    private void Start()
    {
        currentLife = Maxlife;
    }
    private void OnEnable()
    {
        if (isSpecial)
        {
            return;
        }
        else if (isFlying)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (fromLeft)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

    }
    //private void OnDisable()
    //{
    //    transform.position = transform.parent.position;
    //}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (speed) * Time.deltaTime);
        
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathWall"))
        {
            gameObject.SetActive(false);
            //AÑADIR SONIDO DE FALLO
            GameManager.instance.ReduceStrike(1);
        }
    }

    public void TakeDamage(int damage)
    {
        currentLife -= damage;

        if (currentLife <= 0)
        {
            Death();
        }
    }
    private void Death()
    {
        //TODO: GAMEFEEL DE MUERTE
        //Debug.Log("Morí");
        if (isSpecial)
        {
            GameManager.instance.currentGranade += 1;
            EnemyPool.instance.specialSpawned = false;

        }
        Vector3 offsetPosition = transform.position + new Vector3(0, 0.5f, 0);
        GameObject particles = Instantiate(deathParticle, offsetPosition, deathParticle.transform.rotation);
        Destroy(particles, 2f);
        gameObject.SetActive(false);
    }
}
