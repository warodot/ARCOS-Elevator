using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : Health
{
    public int maxHealth = 50;
    private int currentHealth;

    [SerializeField] Animator anim;

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

        onHealthChanged.Invoke(currentHealth);
    }

    /// <summary>
    /// Kills the object and calls onDeath event
    /// </summary>
    public void Die()
    {
        
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
