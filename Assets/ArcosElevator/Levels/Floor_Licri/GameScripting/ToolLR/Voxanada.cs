using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Voxanada : MonoBehaviour
{
    public float explosionRadius = 5f;
    
    public LayerMask damageLayers; 
    public GameObject explosionEffectPrefab; 
    public UnityEvent OnExplosion;
    public float CameraShakeIntensity = 0.05f;
    

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
        OnExplosion?.Invoke();
    }

    void Explode()
    {
        CinemachineShake.Instance.ShakeCamera(CameraShakeIntensity, 0.8f);
                
        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, damageLayers);
        foreach (Collider Object in colliders)
        {
            GroundEnemy enemy = Object.GetComponent<GroundEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(2);
            }
        }
        gameObject.SetActive(false);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
    private void Deactivate()
    {

        gameObject.SetActive(false);
    }
}
