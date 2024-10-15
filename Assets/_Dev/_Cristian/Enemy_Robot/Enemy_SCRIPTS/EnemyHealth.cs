using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VFX;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    [SerializeField] private int currentHealth;
    [SerializeField] Animator anim;
    [SerializeField] List<Rigidbody> ragRigid = new();

    [SerializeField] GameObject shieldEffect;
    [SerializeField] VisualEffect shatteringShield;
    bool hasShieldShattered = true;

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


    public void TriggerVFX()
    {
        if(!hasShieldShattered)
        {
            shatteringShield.Play();
            hasShieldShattered = true;
        }
    }

    public void InstantiateShield(Vector3 shieldPos)
    {
        shieldPos = new Vector3(
            x: shieldPos.x,
            y: shieldPos.y,
            z: shieldPos.z + .04f);
        GameObject shield = Instantiate(shieldEffect, shieldPos, Quaternion.identity, transform.root);
        shield.transform.LookAt(MapPlayerPosManager.instance.GetPlayerRef().transform.position);
        Destroy(shield, 3f);
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

    void AddForce()
    {
        
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}