using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Tool_Voxanator : Tool
{
    public float rayLength = 10;
    [SerializeField] private LayerMask collisionLayers = 1;
    [SerializeField] private AudioClip shootSound;
    [Header("Weapon Stats")]
    public int maxAmmo = 10;
    public int currentAmmo = 10;
    [Header("Numbers display")]
    public TextMeshPro currentAmmoDisplay;
    public TextMeshPro reloadText;
    void Start()
    {
        AddToolFunction(KeyCode.Mouse0, NormalShoot);
        AddToolFunction(KeyCode.Mouse1, Voxanada);
        AddToolFunction(KeyCode.R, Reload);

        currentAmmo = maxAmmo;
    }
    private void FixedUpdate()
    {
        currentAmmo = Mathf.Clamp(currentAmmo, 0, maxAmmo);

        currentAmmoDisplay.text = currentAmmo.ToString();
    }

    void NormalShoot()
    {
        if (currentAmmo > 0)
        {
            PlaySound(shootSound);
            currentAmmo -= 1;
            Ray ray = new Ray(transform.position, transform.forward);

            Debug.DrawLine(ray.origin, ray.origin + ray.direction * 20, Color.red, 10f);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, 20, collisionLayers))
            {
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
        else
        {
            //TODO: AUDIO SIN BALAS
            Debug.Log("No hay más balas");
        }
        
    }

    void Voxanada()
    {

    }
    void Reload()
    {
        StartCoroutine(ReloadCD());
    }
    IEnumerator ReloadCD()
    {
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
        Debug.Log("Arma recargada");
        //TODO: AUDIO DE RECARGA + EFECTO
        currentAmmo = maxAmmo;
    }
    void DrawRay()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        Debug.DrawLine(ray.origin, ray.origin + ray.direction * rayLength, Color.green);
    }
}
