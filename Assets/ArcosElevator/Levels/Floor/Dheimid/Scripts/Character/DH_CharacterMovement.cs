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

    [Space]
    [Space]
    [Header("Cruoched")]
    public float m_crouchCenter = -0.45f;
    public float m_crouchHeight;
    public Transform m_camera;
    public float m_cameraYPos = 0.3f;

    [Space]
    [Space]
    [Header("Gravity related")]
    public float m_gravityForce;
    
    [Space]
    [Space]
    [Header("Sound related")]
    public AudioSource m_source;
    public List<AudioClip> m_clips;
    public float m_speedSteps;

    //Private Variables
    float m_gravity;
    float m_initialCenter;
    float m_initialHeight;
    float m_initialCameraPos = 0.8f;

    //Input
    bool m_isRunning;
    bool m_isCrouching;

    enum CharacterState
    {
        Idle,
        Walking,
        Running,
        Crouching
    }

    CharacterState m_charState;

    void Start() 
    {
        m_initialCenter = m_controller.center.y;
        m_initialHeight = m_controller.height;
    }

    void Update()
    {
        Movement();
        if (DH_GameManager.State == GameStates.Gameplay) Footsteps();
    }   
    
    void Movement()
    {
        //Manejar el inpjut
        m_isRunning = Input.GetKey(KeyCode.LeftShift);
        m_isCrouching = Input.GetKey(KeyCode.LeftControl);
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        //Controlar el estado del personaje
        StateController(x, z);

        //Gravedad
        Gravity();

        //Agachado
        Crouching();
        
        //Manejar velocidades
        float speed = (m_isRunning && !m_isCrouching ? m_runSpeed : m_walkSpeed) * Time.deltaTime;

        //Manejar la nueva dirección y normalizarla
        Vector3 dir = DH_GameManager.State != GameStates.Gameplay ? Vector3.zero : new Vector3(x, m_gravity, z);
        Vector3 normalizedDir = new Vector3(dir.normalized.x * speed, dir.y, dir.normalized.z * speed);

        //Aplicar la dirección al movimiento
        m_controller.Move(transform.TransformDirection(normalizedDir));
    }

    void Gravity()
    {
        //Manejar la Gravedad
        if (!m_controller.isGrounded) m_gravity -= m_gravityForce * Time.deltaTime;
        else m_gravity = 0;
    }

    void StateController(float x, float z)
    {
        //Detectar el estado actual del personaje
        if (Mathf.Abs(x) > 0.1f || Mathf.Abs(z) > 0.1f)
        {
            //Agachados
            if (m_isCrouching) m_charState = CharacterState.Crouching;

            //Caminando, corriendo.
            if (m_charState != CharacterState.Crouching)
            {
                m_charState = CharacterState.Walking;
                if (m_isRunning) m_charState = CharacterState.Running;
            }
        }
        else m_charState = CharacterState.Idle; //Idle
    }

    bool canCrouch;
    void Crouching()
    {
        if (m_isCrouching && !canCrouch) 
        {
            canCrouch = true;
            StopAllCoroutines();
            StartCoroutine(CrouchBehavior(true));
        }
        else if (!m_isCrouching && canCrouch)
        {
            canCrouch = false;
            StopAllCoroutines();
            StartCoroutine(CrouchBehavior(false));
        }
    }

    IEnumerator CrouchBehavior(bool active)
    {
        //Height
        float initial = m_controller.height;
        float target = active ? m_crouchHeight : m_initialHeight;
        
        //Center
        Vector3 initialCenter = m_controller.center;
        Vector3 targetCenter = active ? new Vector3(0, m_crouchCenter, 0) : new Vector3(0, m_initialCenter, 0);
        
        //Camera
        Vector3 initialCamera = m_camera.localPosition;
        Vector3 targetCamera = active ? new Vector3(0, m_cameraYPos, 0) : new Vector3(0, m_initialCameraPos, 0);

        for (float i = 0; i < 0.3f; i += Time.deltaTime)
        {
            float t = i / 0.3f;
            m_controller.height = Mathf.Lerp(initial, target, t);
            m_controller.center = Vector3.Lerp(initialCenter, targetCenter, t);
            m_camera.localPosition = Vector3.Lerp(initialCamera, targetCamera, t);
            yield return null;
        }

        m_controller.height = target;
        m_controller.center = targetCenter;
        m_camera.localPosition = targetCamera;
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
