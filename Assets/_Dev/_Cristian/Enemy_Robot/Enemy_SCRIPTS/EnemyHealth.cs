using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    [SerializeField] private int currentHealth;
    [SerializeField] Animator anim;
    [SerializeField] List<Rigidbody> ragRigid = new();

    [SerializeField] GameObject shieldEffect;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }


    public void InstantiateShield(Vector3 shieldPos, Quaternion shieldRot)
    {

    }

    public void Die()
    {
        GetComponent<NavMeshAgent>().enabled = false;
        anim.enabled = false;
        foreach (Rigidbody rb in ragRigid)
        {
            rb.isKinematic = false;
            rb.useGravity = true;
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}