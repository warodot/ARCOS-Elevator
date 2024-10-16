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
    [SerializeField] GameObject weaponModel;
    bool hasShieldShattered = false;

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
        if (!hasShieldShattered)
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
            z: shieldPos.z + .5f);
        GameObject shield = Instantiate(shieldEffect, shieldPos, Quaternion.identity);
        shield.transform.LookAt(MapPlayerPosManager.instance.transform, Vector3.up);
        Destroy(shield, 3f);
    }

    public void Die()
    {
        EnemiesManager.instance.RemoveEnemy(GetComponent<EnemySM>());
        GetComponent<EnemySM>().StopAllCoroutines();
        anim.enabled = false;
        weaponModel.GetComponentInChildren<Rigidbody>().isKinematic = false;
        weaponModel.GetComponentInChildren<Rigidbody>().useGravity = true;
        weaponModel.GetComponentInChildren<Collider>().enabled = true;
        weaponModel.transform.parent = null;
        foreach (Rigidbody rb in ragRigid)
        {
            rb.AddRelativeForce(GetComponent<NavMeshAgent>().velocity, ForceMode.Impulse);
            rb.isKinematic = false;
            rb.useGravity = true;
            GetComponent<EnemySM>().enabled = false;
            GetComponent<EnemyHealth>().enabled = false;
        }
        GetComponent<NavMeshAgent>().enabled = false;
    }

    public void AddForce(Rigidbody lastHitRB, RaycastHit hit, Vector3 lastRBHitPos, float shootForce)
    {
        Debug.Log("Fired");
        lastHitRB.AddForceAtPosition(-hit.normal * shootForce, lastRBHitPos, ForceMode.Impulse);
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}