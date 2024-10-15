using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.VFX;
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
    [SerializeField] int weaponDamage;
    [SerializeField] bool _canHoldMouse;

    [Header("Weapon Reload Control")]
    [SerializeField] bool _canFire, _isReloading;

    [Header("Animator controller")]
    [SerializeField] Animator _animator;

    [SerializeField] KeyCode _shootingKey, _aimingKey, _reloadKey;

    [Header("Reload SFX Manager")]
    [SerializeField] List<AudioClip> audioClips = new();
    private int _currentSFX;

    [Header("Enemy Manager")]
    [SerializeField] LayerMask whatIsEnemy;

    //FX
    [SerializeField] GameObject bulletHole, metalSpark;
    [SerializeField] VisualEffect muzzleFlash;
    [SerializeField] float shootForce;

    private void Awake()
    {
        _currentAmmo = maxAmmo;
        CreateFirstPlayerPref();
    }

    private void OnEnable()
    {
        _currentAmmo = PlayerPrefs.GetFloat(weaponName);
    }


    void CreateFirstPlayerPref()
    {
        if (!PlayerPrefs.HasKey(weaponName)) PlayerPrefs.SetFloat(weaponName, maxAmmo);
        else return;
    }

    private void Update()
    {
        if(_currentAmmo <= 0)
        {
            muzzleFlash.Stop();
        }

        if (Input.GetKeyDown(_reloadKey) && !_isReloading)
        {
            PlayReloadAnimation();
        }

        if(Input.GetKeyDown(_shootingKey))
        {
            ActivateEffect();
        }
        if (Input.GetKeyUp(_shootingKey))
        {
            muzzleFlash.Stop();
        }

        CheckAmmo();

        if (_canFire)
        {
            Shoot();
        }
        else if (_canFire && Input.GetKeyDown(_shootingKey))
        {
            DryFire();
        }
    }

    void PlayReloadAnimation()
    {
        _isReloading = true;
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

        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hitInfo, 1000f))
        {
            var offsetX = Random.Range(-0.1f, 0.1f);
            var offsetZ = Random.Range(-0.1f, 0.1f);
            var offsetY = Random.Range(-0.1f, 0.1f);
            var offsetPos = new Vector3(
                x: hitInfo.point.x + offsetX,
                y: hitInfo.point.y + offsetY,
                z: hitInfo.point.z + offsetZ);

            if (hitInfo.collider.CompareTag("Enemy"))
            {
                var enemy = hitInfo.transform.root.GetComponent<EnemyHealth>();
                enemy.TakeDamage(weaponDamage);
                if(enemy.GetHealth() - weaponDamage < 0)
                {
                    enemy.AddForce(hitInfo.transform.GetComponent<Rigidbody>(), hitInfo, hitInfo.point, shootForce);
                }

                if(enemy.GetHealth() - weaponDamage <= weaponDamage)
                {
                    enemy.TriggerVFX();
                }
                if(enemy.GetHealth() > weaponDamage)
                {
                    enemy.InstantiateShield(hitInfo.point);
                }
                else
                {
                    Instantiate(metalSpark, offsetPos, Quaternion.LookRotation(hitInfo.normal, Vector3.up));
                }
            }
            else
            {
                
                Instantiate(bulletHole, offsetPos, Quaternion.LookRotation(hitInfo.normal, Vector3.up));
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

    void ActivateEffect()
    {
        if (!muzzleFlash.HasAnySystemAwake())
        {
            muzzleFlash.Play();
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

    void ResetReloadBool()
    {
        _isReloading = false;
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(weaponName, _currentAmmo);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(Camera.main.transform.position, Camera.main.transform.forward);
    }
}
