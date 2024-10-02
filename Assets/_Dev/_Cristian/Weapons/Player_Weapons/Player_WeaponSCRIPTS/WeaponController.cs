using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Manager")]
    [SerializeField] string weaponName;

    [Header("SFX Control")]
    [SerializeField] AudioSource _weaponSource;
    [SerializeField] AudioClip _firingClip, _dryFireClip;

    [Header("Weapon Firing Control")]
    [SerializeField] float _timeBetweenShots;
    [SerializeField] float _currentAmmo, maxAmmo, _ammoLeftLastUsage;
    float var;
    [SerializeField] bool _canHoldMouse;
    Transform _playerRef;

    [Header("Weapon Reload Control")]
    [SerializeField] bool _isAmmoEmpty, _canFire;

    [Header("Animator controller")]
    [SerializeField] Animator _animator;

    [SerializeField] KeyCode _shootingKey, _aimingKey, _reloadKey;

    private void Awake()
    {
        _currentAmmo = maxAmmo;
        CreateFirstPlayerPref();
    }

    private void OnEnable()
    {
        _currentAmmo = PlayerPrefs.GetFloat(weaponName);
        _playerRef = MapPlayerPosManager.instance.GetPlayerRef().transform;
    }

    void CreateFirstPlayerPref()
    {
        if (!PlayerPrefs.HasKey(weaponName)) PlayerPrefs.SetFloat(weaponName, maxAmmo);
        else return;
    }

    private void Update()
    {
        if (Input.GetKeyDown(_reloadKey))
        {
            PlayReloadAnimation();
        }

        CheckAmmo();

        if (_canFire)
        {
            Shoot();
        }
        else if(_canFire && Input.GetKeyDown(_shootingKey))
        {
            DryFire();
        }
    }

    void PlayReloadAnimation()
    {
        _animator.SetTrigger("Reload");
    }

    void SetAmmo()
    {
        _currentAmmo = maxAmmo;
    }

    void CheckAmmo()
    {
        if (_currentAmmo <= 0)
        {
            _canFire = false;
            _isAmmoEmpty = true;
        }
        else _canFire = true;
    }

    void DryFire()
    {
        _weaponSource.PlayOneShot(_dryFireClip);
    }

    void Shoot()
    {
        if (_canHoldMouse)
        {
            if (Input.GetKey(_shootingKey))
            {
                Fire();
            }
        }
        else
        {
            if (Input.GetKeyDown(_shootingKey))
            {
                Fire();
            }

        }
    }

    void Fire()
    {
        if (_canHoldMouse)
        {
            var += Time.deltaTime;
            if (var > _timeBetweenShots)
            {
                _weaponSource.PlayOneShot(_firingClip);
                _currentAmmo--;
                var = 0;
            }
        }
        else
        {
            _weaponSource.PlayOneShot(_firingClip);
            _currentAmmo--;
        }
        _animator.SetTrigger("Fire");

    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(weaponName, _currentAmmo);
    }
}
