using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class DH_CharacterMovement : MonoBehaviour
{
    //Public Variables
    [Header("Movement related")]
    public CharacterController m_controller;
    public float m_walkSpeed;
    public float m_runSpeed;
    public float m_gravityForce;
    
    [Space]

    [Header("Sound related")]
    public AudioSource m_source;
    public List<AudioClip> m_clips;
    public float m_speedSteps;

    //Private Variables
    float m_gravity;
    bool m_isRunning;

    enum CharacterState
    {
        Idle,
        Walking,
        Running,
        Crouching
    }

    CharacterState m_charState;

    void Update()
    {
        Movement();
        Footsteps();
    }   
    
    void Movement()
    {
        //Manejar el inpjut
        m_isRunning = Input.GetKey(KeyCode.LeftShift);
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //Detectar el estado actual
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            m_charState = CharacterState.Walking;
            if (m_isRunning) m_charState = CharacterState.Running;
        }
        else 
        {
            m_charState = CharacterState.Idle;
        }

        Debug.Log(m_charState);

        //Manejar la Gravedad
        if (!m_controller.isGrounded) m_gravity -= m_gravityForce * Time.deltaTime;
        else m_gravity = 0;
        
        //Manejar velocidades
        float speed = (m_isRunning ? m_runSpeed : m_walkSpeed) * Time.deltaTime;

        //Manejar la nueva dirección y normalizarla
        Vector3 dir = DH_GameManager.State != GameStates.Gameplay ? Vector3.zero : new Vector3(x, m_gravity, z);
        Vector3 normalizedDir = new Vector3(dir.normalized.x * speed, dir.y, dir.normalized.z * speed);

        //Aplicar la dirección al movimiento
        m_controller.Move(transform.TransformDirection(normalizedDir));
    }

    float constantValue = 0;
    float currentStepSpeed = 0;
    void Footsteps()
    {
        switch(m_charState)
        {
            case CharacterState.Walking: currentStepSpeed = m_speedSteps; break;
            case CharacterState.Running: currentStepSpeed = m_speedSteps / 2; break;
            case CharacterState.Crouching: currentStepSpeed = m_speedSteps * 2; break;
            case CharacterState.Idle: currentStepSpeed = 0; break;
        }

        if (m_charState != CharacterState.Idle)
        {
            constantValue += Time.deltaTime;
            if (constantValue > currentStepSpeed)
            {
                constantValue = 0;
                int randomValue = UnityEngine.Random.Range(0, m_clips.Count);
                m_source.PlayOneShot(m_clips[randomValue]);
            }
        }
    }
}
