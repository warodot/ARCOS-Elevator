using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_Voxanator : Tool
{
    public float rayLength = 10;
    [SerializeField] private LayerMask collisionLayers = 1;
    [SerializeField] private AudioClip shootSound;
    void Start()
    {
        AddToolFunction(KeyCode.Mouse0, NormalShoot);
        AddToolFunction(KeyCode.Mouse1, Voxanada);
    }

    void NormalShoot()
    {
        PlaySound(shootSound);
        Ray ray = new Ray(transform.position, transform.forward);

        Debug.DrawLine(ray.origin, ray.origin + ray.direction * 10, Color.red, 10f);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10, collisionLayers))
        {
            //Quisiera que este script acceda al componente "GroundEnemy" y ejecute el metodo TakeDamage(1)
            Debug.Log("Le he dado a: " + hitInfo.collider.gameObject.name);

            GroundEnemy enemy = hitInfo.collider.GetComponent<GroundEnemy>();

            if (enemy != null)
            {
                
                enemy.TakeDamage(1);
                //Debug.Log("¡Daño infligido!");
            }
            else
            {
                //Debug.Log("No es un enemigo válido.");
            }
        }
    }

    void Voxanada()
    {

    }
    void DrawRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayLength, Color.green);
    }
}
