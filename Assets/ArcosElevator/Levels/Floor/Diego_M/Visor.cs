using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Visor : MonoBehaviour
{
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform shootingPoint;
    [SerializeField] private float fireRate = 0.3f; 

    private float nextFireTime = 0f;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && Time.time >= nextFireTime)
        {
            ShootLaser();
            nextFireTime = Time.time + fireRate;
        }
    }

    void ShootLaser()
    {
        Instantiate(laserPrefab, shootingPoint.position, shootingPoint.rotation);
    }
}
