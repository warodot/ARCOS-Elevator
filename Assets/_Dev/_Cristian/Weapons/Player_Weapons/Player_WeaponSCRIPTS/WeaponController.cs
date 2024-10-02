using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class WeaponController : MonoBehaviour
{
    [Header("Weapon Manager")]
    [SerializeField] string weaponName;

    [Header("SFX Control")]
    [SerializeField] AudioSource _weaponSource, _reloadSource;
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

    [Header("Reload SFX Manager")]
    [SerializeField] List<AudioClip> audioClips = new();
    private int _currentSFX;

    [Header("Enemy Manager")]
    [SerializeField] LayerMask whatIsEnemy;

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

    void PlayFireSFX()
    {
        _weaponSource.PlayOneShot(_firingClip);
    }


    void FireRaycast()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward);
        if(Physics.Raycast(Camera.main.transform.position, transform.forward, out RaycastHit hitinfo, 1000f, whatIsEnemy))
        {
            if(hitinfo.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy Hit");
            }
            else
            {
                Debug.Log("Ambient Hit");
            }
        }
        else
        {
            Debug.Log("No Hit");
        }
    }


    void Fire()
    {
        if (_canHoldMouse)
        {
            var += Time.deltaTime;
            if (var > _timeBetweenShots)
            {
                _animator.SetTrigger("Fire");
                FireRaycast();
                _currentAmmo--;
                var = 0;
            }
        }
        else
        {
            _animator.SetTrigger("Fire");
            FireRaycast();
            _currentAmmo--;
        }
    }

    void PlayReloadSFX()
    {
        _reloadSource.Stop();
        _reloadSource.clip = audioClips[_currentSFX];
        _reloadSource.Play();
        _currentSFX++;
    }

    void ResetSFX()
    {
        _currentSFX = 0;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(weaponName, _currentAmmo);
    }
}
