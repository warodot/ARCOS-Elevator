using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
  public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] float timeSinceLastDamage;

    [Header("Events")]
    public UnityEvent onDeath;
    public IntEvent onHealthChanged;

    void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// Deals damage to the object and calls onHealthChanged event
    /// </summary>

    public void TakeDamage(int damage)
    {
        timeSinceLastDamage = 0;
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }

        onHealthChanged.Invoke(currentHealth);
    }


    void Update()
    {
        HealhWithTime();
    }


    void HealhWithTime()
    {
        timeSinceLastDamage += Time.deltaTime;
        if(timeSinceLastDamage > 10 && currentHealth < maxHealth)
        {   
            Heal(20);
            timeSinceLastDamage = 9.5f;
        }
    }

    /// <summary>
    /// Heals the object and calls onHealthChanged event
    /// </summary>

    public void Heal(int amount)
    {
        currentHealth += amount;

        if(currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }

        onHealthChanged.Invoke(currentHealth);
    }

    /// <summary>
    /// Kills the object and calls onDeath event
    /// </summary>
    public void Die()
    {
        onDeath.Invoke();
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

        onHealthChanged.Invoke(currentHealth);
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}   