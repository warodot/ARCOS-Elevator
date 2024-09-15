using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class DH_CharacterMovement : MonoBehaviour
{
    //Public Variables
    public CharacterController m_controller;
    public float m_walkSpeed;
    public float m_runSpeed;
    public float m_gravityForce;

    //Private Variables
    float m_gravity;
    bool m_isRunning;

    void Update() => Movement();
    
    void Movement()
    {
        //Manejar el inpjut
        m_isRunning = Input.GetKey(KeyCode.LeftShift);
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

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
}
