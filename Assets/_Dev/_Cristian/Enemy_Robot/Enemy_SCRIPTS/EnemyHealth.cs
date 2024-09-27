using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 50;
    [SerializeField] private int currentHealth;

    [SerializeField] GameObject playableGameObject, ragdollGameObject;

    void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Deals damage to the object and calls onHealthChanged event
    /// </summary>

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

    }

    /// <summary>
    /// Kills the object and calls onDeath event
    /// </summary>
    public void Die()
    {
        GetComponent<EnemySM>().enabled = false;
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<CapsuleCollider>().enabled = false;
        playableGameObject.SetActive(false);
        ragdollGameObject.SetActive(true);
    }

    /// <summary>
    /// Sets health directly and calls onHealthChanged event
    /// </summary>
    public void SetHealth(int health)
    {
        currentHealth = health;

        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        else if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}
