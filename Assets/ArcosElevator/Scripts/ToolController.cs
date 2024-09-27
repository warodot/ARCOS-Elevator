using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    public WeaponData weaponData;
    private int bulletsLeft;

    private bool shooting, readyToShoot, reloading, aiming;
    public GameObject fpsCam;
    public RaycastHit raycastHit;
    public LayerMask enemyLayer;
    public AudioSource weaponAudioSource;
    public GameObject bulletHoleDecal;
    public Animator weaponAnimationController;

    void Start()
    {
        bulletsLeft = weaponData.magazineSize;
        readyToShoot = true;

    }

    void Update()
    {
        WeaponInput();
    }


    private void WeaponInput()
    {
        if (weaponData.canHoldButton)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < weaponData.magazineSize && !reloading)
        {
            Reload();
        }
        if(Input.GetKey(KeyCode.Mouse1))
        {
            Aim();
        }
        else if(Input.GetKeyUp(KeyCode.Mouse1))
        {
            weaponAnimationController.SetBool("Aiming", false);
        }

        if (readyToShoot && shooting && !reloading && bulletsLeft > 0)
        {
            Shoot();
        }
        else if (readyToShoot && shooting && !reloading && bulletsLeft <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                weaponAudioSource.PlayOneShot(weaponData.emptySFX);
            }
        }
    }

    /// <summary>
    /// Starts the reload process
    /// </summary>
    private void Reload()
    {
        reloading = true;
        weaponAudioSource.pitch = 1f;
        //weaponAudioSource.PlayOneShot(weaponData.reloadSFX);
        weaponAnimationController.SetTrigger("Reloading");
        Invoke("ReloadFinished", weaponData.reloadTime);
    }

    private void Aim()
    {
        aiming = true;
        weaponAnimationController.SetBool("Aiming", true);
    }

    /// <summary>
    /// Finishes the reload process
    /// </summary>
    private void ReloadFinished()
    {
        bulletsLeft = weaponData.magazineSize;
        reloading = false;
    }

    /// <summary>
    /// Shoots the weapon
    /// </summary>
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-weaponData.spread, weaponData.spread);
        float y = Random.Range(-weaponData.spread, weaponData.spread);

        //Calculate direction with spread
        Vector3 _shot_direction = fpsCam.transform.forward + new Vector3(x, y, 0);


        //Check raycast
        if (Physics.Raycast(fpsCam.transform.position, _shot_direction, out raycastHit, weaponData.range, enemyLayer))
        {
            EnemyHealth health = raycastHit.collider.GetComponent<EnemyHealth>();

            if (health)
            {
                health.TakeDamage(weaponData.damage);
            }
        }

        bulletsLeft -= 1;
        Invoke("ResetShot", weaponData.timeBetweenShots);
        weaponAnimationController.SetTrigger("Fire");

        CinemachineShake.Instance.ShakeCamera(weaponData.cameraShakeIntensity, weaponData.cameraShakeTime);
        weaponAudioSource.pitch = Random.Range(0.90f, 1.10f);
        weaponAudioSource.PlayOneShot(weaponData.shootSFX);
        Vector3 decalOffset = raycastHit.normal * 0.01f;
        Instantiate(bulletHoleDecal, raycastHit.point + decalOffset, Quaternion.LookRotation(-raycastHit.normal));
    }

    /// <summary>
    /// Resets the readyToShoot variable, allowing the weapon to shoot again
    /// </summary>
    private void ResetShot()
    {
        readyToShoot = true;
        if (bulletsLeft <= 0 && shooting)
        {
            weaponAudioSource.PlayOneShot(weaponData.emptySFX);
        }
    }
}
