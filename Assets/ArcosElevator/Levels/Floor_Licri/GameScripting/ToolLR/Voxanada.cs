using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Voxanada : MonoBehaviour
{
    public float explosionRadius = 5f;
    
    public LayerMask damageLayers; 
    public GameObject explosionEffectPrefab; 

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

    void Explode()
    {
        // Instanciar el efecto de explosión en la posición de la granada
        Instantiate(explosionEffectPrefab, transform.position, transform.rotation);

        // Detectar objetos en el radio de la explosión
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, damageLayers);
        foreach (Collider Object in colliders)
        {
            
            // Aplicar daño si es necesario
            GroundEnemy enemy = Object.GetComponent<GroundEnemy>();
            if (enemy != null)
            {
                enemy.TakeDamage(2);
            }
        }

        // Destruir la granada después de la explosión
        Destroy(gameObject);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
