using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using LucasRojo;

public class Tool_Voxanator : Tool
{
    public float rayLength = 10;
    [SerializeField] private LayerMask collisionLayers = 1;
    public Animator anim;
    
    [Header("Weapon Stats")]
    public int maxAmmo = 10;
    public int currentAmmo = 10;
    public bool canShoot = true;
    public bool isShooting = false;
    
    [Header("Voxanada")]
    public LayerMask voxLayers;
    public GameObject voxPrefab;
    public Transform voxSpawnPoint;
    public GameObject granade1;
    public GameObject granade2;
    public GameObject granade3;
    public int voxSpeed = 10;
    [Header("Numbers display")]
    public TextMeshPro currentAmmoDisplay;
    public TextMeshPro reloadText;
    [Header("References")]
    public ParticleSystem shootParticle;
    [Header("Sounds")]
    public AudioClip error;
    [SerializeField] private AudioClip shootSound;
    public AudioClip reload;
    public AudioClip bullet;
    

    void Start()
    {
        AddToolFunction(KeyCode.Mouse0, NormalShoot);
        AddToolFunction(KeyCode.Mouse1, Voxanada);
        AddToolFunction(KeyCode.R, Reload);

        currentAmmo = maxAmmo;
        GameManager.instance.currentGranade = GameManager.instance.maxGranade;
        
    }
    private void FixedUpdate()
    {
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);
        GameManager.instance.currentGranade = Mathf.Clamp(GameManager.instance.currentGranade, 0, GameManager.instance.maxGranade);

        currentAmmoDisplay.text = currentAmmo.ToString();
        if (GameManager.instance.currentGranade == 1)
        {
            granade1.SetActive(true);
            granade2.SetActive(false);
            granade3.SetActive(false);
        }
        else if (GameManager.instance.currentGranade == 2)
        {
            granade1.SetActive(true);
            granade2.SetActive(true);
            granade3.SetActive(false);
        }
        else if (GameManager.instance.currentGranade == 3)
        {
            granade1.SetActive(true);
            granade2.SetActive(true);
            granade3.SetActive(true);
        }
        else
        {
            granade1.SetActive(false);
            granade2.SetActive(false);
            granade3.SetActive(false);
        }
    }

    void NormalShoot()
    {
        if (currentAmmo > 0 && canShoot && !isShooting)
        {
            StartCoroutine(ShootCD());
            anim.Play("ShootAnim");
            PlaySound(shootSound);
            currentAmmo -= 1;
            shootParticle.Play();
            Ray ray = new Ray(transform.position, transform.forward);

            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 40, Color.red, 10f);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 40, collisionLayers))
            {
                Debug.Log("Le he dado a: " + hitInfo.collider.gameObject.name);

                GroundEnemy enemy = hitInfo.collider.GetComponent<GroundEnemy>();

                if (enemy != null)
                {
                    PlaySound(bullet);
                    enemy.TakeDamage(1);
                    //Debug.Log("Daño infligido");
                }
                else
                {
                    //Debug.Log("No es un enemigo válido.");
                }
            }
        }
        else if (currentAmmo <= 0 && canShoot)
        {
            PlaySound(error);
            Reload();
        }
        
    }

    void Voxanada()
    {
        if (GameManager.instance.currentGranade > 0 && GameManager.instance.canUseGranade)
        {
            // TODO: AUDIO
            GameManager.instance.currentGranade -= 1;
            //GameObject grenade = Instantiate(voxPrefab, voxSpawnPoint.position, voxSpawnPoint.rotation);

            //Rigidbody rb = grenade.GetComponent<Rigidbody>();
            //rb.velocity = voxSpawnPoint.forward * voxSpeed;

            Ray ray = new Ray(transform.position, transform.forward); // Usamos la misma lógica de disparo para el raycast

            if (Physics.Raycast(ray, out RaycastHit hitInfo, rayLength, voxLayers))
            {
                GameObject grenade = Instantiate(voxPrefab, voxSpawnPoint.position, voxSpawnPoint.rotation);
                Rigidbody rb = grenade.GetComponent<Rigidbody>();

                Vector3 direction = (hitInfo.point - voxSpawnPoint.position).normalized;

                rb.velocity = direction * voxSpeed;
            }
        }
        else
        {
            PlaySound(error);
        }
        
    }
    void Reload()
    {
        if(currentAmmo >= maxAmmo ) return;
        StartCoroutine(ReloadCD());
    }
    IEnumerator ReloadCD()
    {
        canShoot = false;
        currentAmmoDisplay.gameObject.SetActive(false);
        reloadText.gameObject.SetActive(true);
        reloadText.text = "Please wait.";
        yield return new WaitForSeconds(0.65f);
        reloadText.text = "Please wait..";
        yield return new WaitForSeconds(0.65f);
        reloadText.text = "Please wait...";
        yield return new WaitForSeconds(0.65f);
        currentAmmoDisplay.gameObject.SetActive(true);
        reloadText.gameObject.SetActive(false);
        canShoot = true;
        Debug.Log("Arma recargada");
        PlaySound(reload);
        currentAmmo = maxAmmo;
    }
    IEnumerator ShootCD()
    {
        isShooting = true;
        yield return new WaitForSeconds(0.5f);
        isShooting = false;
    }
    void DrawRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayLength, Color.green);
    }
}
