using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ArcosElevator/Weapon", order = 1)]
public class WeaponData : ScriptableObject
{
    public string weaponName;

    public int damage;
    public float timeBetweenShots;
    public float spread;
    public float range;
    public float reloadTime;
    public int magazineSize;
    public int bulletsPerShot;
    public bool canHoldButton;

    public float cameraShakeIntensity;
    public float cameraShakeTime;
    public Vector3 position;
    public Mesh mesh;
    public AudioClip shootSFX;
    public AudioClip reloadSFX;
    public AudioClip emptySFX;
}
