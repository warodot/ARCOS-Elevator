using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
  public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] float timeSinceLastDamage;
    [SerializeField] TMPro.TMP_Text m_TextMeshPro;

    [Header("Events")]
    public UnityEvent onDeath;
    public IntEvent onHealthChanged;

    public AudioSource source;
    public AudioClip healthClip;

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

    public void UpdateUI()
    {
        m_TextMeshPro.text = currentHealth.ToString("0") + "%";
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


    public void PlaySFX()
    {
        if(!source.isPlaying) source.PlayOneShot(healthClip);
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

    public void PlayDeathAnim()
    {
        GetComponentInChildren<Animator>().Play("DeathAnim");
    }

    public int GetHealth()
    {
        return currentHealth;
    }
}