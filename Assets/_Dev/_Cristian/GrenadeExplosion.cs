using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrenadeExplosion : MonoBehaviour
{
    [SerializeField] GameObject effectPrefab;
    [SerializeField] float grenadeRadius, grenadeExplosionTime;
    [SerializeField] int grenadeDamage;
    [SerializeField] LayerMask whatIsPlayer;

    private void OnCollisionEnter(Collision collision)
    {
        StartCoroutine(ExplodeGrenade());
    }

    IEnumerator ExplodeGrenade()
    {
        yield return new WaitForSeconds(grenadeExplosionTime);
        Collider[] colliderHits = Physics.OverlapSphere(transform.position, grenadeRadius, whatIsPlayer);
        if (colliderHits.Length > 0)
        {
            for (int i = 0; i < colliderHits.Length; i++)
            {
                if(Physics.Linecast(transform.position, colliderHits[i].transform.position))
                {
                    colliderHits[i].GetComponent<Health>().TakeDamage(grenadeDamage);
                    break;
                }
            }
        }
        Instantiate(effectPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
        yield break;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grenadeRadius);
    }
}
