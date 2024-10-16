using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivarAnimacion : MonoBehaviour
{
    public Animator anim;
    public AudioSource audioSource;

    void Start()
    {
        
    }

    public void ActivarAnimacionPersonaje()
    {
        anim.SetBool("Aplastar", true);
        audioSource.PlayOneShot(audioSource.clip);
    }
}
