using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectricFloor : MonoBehaviour
{
    public float damagePerSecond = 5f;
    public Vector3 boxSize = new Vector3(0.5f, 0.5f, 0.5f);

    private void Update()
    {
        CheckForPlayer();
    }

    private void CheckForPlayer()
    {
        Vector3 center = transform.position + Vector3.up * 0.15f;
        Collider[] hitColliders = Physics.OverlapBox(center, boxSize, Quaternion.identity);

        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Player"))
            {
                Health playerHealth = hitCollider.GetComponent<Health>();

                if (playerHealth != null)
                {
                    int damage = Mathf.RoundToInt(damagePerSecond);
                    playerHealth.TakeDamage(damage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position + Vector3.up * 0.5f, boxSize * 2);
    }
}
