using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HurtZone : MonoBehaviour
{
    [Tooltip("How much damage to deal each tick")]
    public int damagePerTick = 10;
    [Tooltip("Time in seconds between each tick")]
    public float tickFrequency = 1f;
    [Tooltip("Does it damage the object as soon as it touches the trigger?")]
    public bool canDamageOnEnter;


    void OnTriggerEnter(Collider other)
    {
        Health health = other.gameObject.GetComponent<Health>();

        if (health)
        {
            StartCoroutine(DamageOverTime(health));
        }
    }

    void OnTriggerExit(Collider other)
    {
        Health health = other.gameObject.GetComponent<Health>();
        if (health)
        {
            StopCoroutine(DamageOverTime(health));
        }
    }

    private IEnumerator DamageOverTime(Health health_to_damage)
    {
        while (true)
        {
            health_to_damage.TakeDamage(damagePerTick);
            yield return new WaitForSeconds(tickFrequency);
        }
    }
}
