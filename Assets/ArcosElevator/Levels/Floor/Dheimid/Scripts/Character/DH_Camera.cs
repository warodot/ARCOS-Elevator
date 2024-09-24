using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_Camera : MonoBehaviour
{
    public GameObject m_camera;
    public float m_sensitivity;
    public float m_yMin, m_yMax;
    
    void Update() => Camera();

    float yCurrentCamera;
    void Camera()
    {
        //Manejar el input
        float x = Input.GetAxis("Mouse X");
        float y = Input.GetAxis("Mouse Y");

        //Manejar la dirección de movimiento
        Vector3 dir = DH_GameManager.State != GameStates.Gameplay ? Vector3.zero : new Vector2(x, y);

        //En relación al eje Y
        yCurrentCamera -= dir.y;
        yCurrentCamera = Mathf.Clamp(yCurrentCamera, m_yMin, m_yMax);
        m_camera.transform.localEulerAngles = new Vector3(yCurrentCamera, 0, 0); 

        //En relación al eje x
        transform.Rotate(Vector3.up, dir.x);
    }
}
