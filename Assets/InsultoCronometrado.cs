using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InsultoCronometrado : MonoBehaviour
{
    public AudioSource insulto;
    public float temporizador;
    public float tiempoDeCadaInsulto;

    // Update is called once per frame
    void Update()
    {
        if(temporizador <= 0)
        {
            insulto.Play();
            temporizador = tiempoDeCadaInsulto;
        }

        temporizador -= Time.deltaTime;
    }
}
