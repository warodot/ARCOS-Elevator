using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LibreriaDeSonidos : MonoBehaviour
{
    public AudioClip[] sonidos;  
    public AudioSource audioSource;

    public AudioClip sonidoEspecifico;
    public AudioSource sonidoEspecificoSource;


  
    public void ReproducirSonidoAleatorio()
    {
        if (sonidos.Length == 0) return;  
        int indexAleatorio = Random.Range(0, sonidos.Length);  
        AudioClip clipSeleccionado = sonidos[indexAleatorio];  
        audioSource.PlayOneShot(clipSeleccionado); 
    }

    public void ReproducirSonidoEspecifico()
    {
        if(sonidoEspecifico != null)
        {
            sonidoEspecificoSource.PlayOneShot(sonidoEspecifico);
        }
    }
}
