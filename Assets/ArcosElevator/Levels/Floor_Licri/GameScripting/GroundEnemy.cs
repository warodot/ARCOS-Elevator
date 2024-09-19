using LucasRojo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GroundEnemy : MonoBehaviour
{
    public bool fromLeft = true;
    public float speed = 2;
    // Start is called before the first frame update
    private void OnEnable()
    {
        transform.position = transform.parent.position;
        //ORIENTACION INICIAL
        if (fromLeft)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        //VELOCIDAD DEPENDIENTE DE LA RONDA
        if (GameManager.instance.round == 2)
        {
            speed = 3;
        }

    }
    //private void OnDisable()
    //{
    //    transform.position = transform.parent.position;
    //}

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (speed) * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DeathWall"))
        {
            gameObject.SetActive(false);
            //AÑADIR SONIDO DE FALLO
            //SUMAR 1 STRIKE AL JUGADOR
        }
    }
}
