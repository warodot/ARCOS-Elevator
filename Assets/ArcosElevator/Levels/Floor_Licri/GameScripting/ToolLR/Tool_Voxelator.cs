using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_Voxelator : Tool
{
    [SerializeField] private LayerMask collisionLayers = 1;
    [SerializeField] private AudioClip shootSound;
    void Start()
    {
        AddToolFunction(KeyCode.Mouse0, NormalShoot);
        AddToolFunction(KeyCode.Mouse1, Voxanada);
    }

    void NormalShoot()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10, collisionLayers))
        {
            Debug.Log("Le he dado a: " + hitInfo.collider.gameObject.name);
            PlaySound(shootSound);
        }
    }

    void Voxanada()
    {

    }
}
