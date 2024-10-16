using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{

    public AudioSource footstepSource; // El AudioSource que reproducir� los pasos.
    public AudioClip[] footstepClips;  // Un array de clips de audio para los pasos.
    public float stepInterval = 0.5f;  // Intervalo entre pasos (en segundos).

    private Vector3 lastPosition;      // Almacena la posici�n previa del personaje.
    private float stepTimer;           // Controla el tiempo entre pasos.

    public float groundCheckDistance = 0.2f; // Distancia para chequear el suelo.
    public LayerMask groundLayer;
    

    void Start()
    {
        
        lastPosition = transform.position;

        
        stepTimer = 0;
    }

    void Update()
    {
        
        if (IsGrounded())
        {
            
            if (transform.position != lastPosition)
            {
                if (transform.position.x != lastPosition.x || transform.position.y != lastPosition.y)
                {
                    
                    stepTimer -= Time.deltaTime;

                    if (stepTimer <= 0)
                    {
                        
                        PlayRandomFootstep();

                        // Restablece el temporizador de los pasos.
                        stepTimer = stepInterval;
                    }
                }
            }
        }

        // Actualiza la �ltima posici�n para la siguiente comprobaci�n.
        lastPosition = transform.position;
    }

    // Funci�n para reproducir un sonido aleatorio de pasos.
    void PlayRandomFootstep()
    {
        if (footstepClips.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepClips.Length);
            footstepSource.PlayOneShot(footstepClips[randomIndex]);
        }
    }

    // Funci�n para verificar si el personaje est� en el suelo usando Raycast.
    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);
    }


}
