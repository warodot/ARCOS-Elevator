using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public List<WeaponData> weaponData;
    private WeaponData _selectedWeaponData;
    public List<GameObject> weaponModel;
    private int bulletsLeft;
    [SerializeField] int _currentWeapon = 0;
  
    [SerializeField] bool _isChangingWeapon;

    private bool shooting, readyToShoot, reloading, aiming;
    public GameObject fpsCam;
    public RaycastHit raycastHit;
    public LayerMask enemyLayer;

    public AudioSource weaponAudioSource;
    public GameObject bulletHoleDecal;
    public Animator weaponAnimationController;
    

    void Start()
    {
        _selectedWeaponData = weaponData[_currentWeapon];
        weaponModel[_currentWeapon].SetActive(true);
        bulletsLeft = _selectedWeaponData.magazineSize;
        readyToShoot = true;

    }

    void Update()
    {
        WeaponInput();
        ChangeWeapon();
    }


    void ChangeWeapon()
    {
        if (!_isChangingWeapon)
        {
            if (Input.mouseScrollDelta.y > 0)
            {
                _isChangingWeapon = true;
                ChangeWeaponUp();
                _isChangingWeapon = false;
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                _isChangingWeapon = true;
                ChangeWeaponDown();
                _isChangingWeapon = false;
            }
        }
    }


    void ChangeWeaponUp()
    {
        if (_currentWeapon == weaponData.Count - 1)
        {
            weaponModel[_currentWeapon].SetActive(false);
            _currentWeapon = 0;
            _selectedWeaponData = weaponData[_currentWeapon];
            weaponModel[_currentWeapon].SetActive(true);
            weaponAnimationController = weaponModel[_currentWeapon].GetComponent<Animator>();
            return;
        }
        weaponModel[_currentWeapon].SetActive(false);
        _currentWeapon++;
        _selectedWeaponData = weaponData[_currentWeapon];
        weaponAnimationController = weaponModel[_currentWeapon].GetComponent<Animator>();
        weaponModel[_currentWeapon].SetActive(true);
    }

    void ChangeWeaponDown()
    {
        if (_currentWeapon == 0)
        {
            weaponModel[_currentWeapon].SetActive(false);
            _currentWeapon = weaponData.Count - 1;
            _selectedWeaponData = weaponData[_currentWeapon];
            weaponModel[_currentWeapon].SetActive(true);
            weaponAnimationController = weaponModel[_currentWeapon].GetComponent<Animator>();
            return;
        }
        weaponModel[_currentWeapon].SetActive(false);
        _currentWeapon--;
        _selectedWeaponData = weaponData[_currentWeapon];
        weaponAnimationController = weaponModel[_currentWeapon].GetComponent<Animator>();
        weaponModel[_currentWeapon].SetActive(true);
    }

    private void WeaponInput()
    {
        if (_selectedWeaponData.canHoldButton)
        {
            shooting = Input.GetKey(KeyCode.Mouse0);
        }
        else
        {
            shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (Input.GetKeyDown(KeyCode.R) && bulletsLeft < _selectedWeaponData.magazineSize && !reloading)
        {
            Reload();
        }
        if (Input.GetKey(KeyCode.Mouse1))
        {
            Aim();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
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
                weaponAudioSource.PlayOneShot(_selectedWeaponData.emptySFX);
            }
        }
    }


    private void Reload()
    {
        reloading = true;
        weaponAudioSource.pitch = 1f;
        //weaponAudioSource.PlayOneShot(weaponData.reloadSFX);
        weaponAnimationController.SetTrigger("Reloading");
        Invoke("ReloadFinished", _selectedWeaponData.reloadTime);
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
        bulletsLeft = _selectedWeaponData.magazineSize;
        reloading = false;
    }

    /// <summary>
    /// Shoots the weapon
    /// </summary>
    private void Shoot()
    {
        readyToShoot = false;

        //Spread
        float x = Random.Range(-_selectedWeaponData.spread, _selectedWeaponData.spread);
        float y = Random.Range(-_selectedWeaponData.spread, _selectedWeaponData.spread);

        //Calculate direction with spread
        Vector3 _shot_direction = fpsCam.transform.forward + new Vector3(x, y, 0);


        //Check raycast
        if (Physics.Raycast(fpsCam.transform.position, _shot_direction, out raycastHit, _selectedWeaponData.range, enemyLayer))
        {
            EnemyHealth health = raycastHit.collider.GetComponent<EnemyHealth>();

            if (health)
            {
                health.TakeDamage(_selectedWeaponData.damage);
            }
        }

        bulletsLeft -= 1;
        Invoke("ResetShot", _selectedWeaponData.timeBetweenShots);
        weaponAnimationController.SetTrigger("Fire");

        CinemachineShake.Instance.ShakeCamera(_selectedWeaponData.cameraShakeIntensity, _selectedWeaponData.cameraShakeTime);
        weaponAudioSource.pitch = Random.Range(0.90f, 1.10f);
        weaponAudioSource.PlayOneShot(_selectedWeaponData.shootSFX);
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
            weaponAudioSource.PlayOneShot(_selectedWeaponData.emptySFX);
        }
    }
}
