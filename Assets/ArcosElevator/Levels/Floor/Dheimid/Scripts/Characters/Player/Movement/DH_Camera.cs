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

    public void LookAt(Transform target) => StartCoroutine(LookAtTarget(target));

    IEnumerator LookAtTarget(Transform target)
    {
        DH_GameManager.State = GameStates.Interacting;

        //Camera --------------------------

            float camInitial = yCurrentCamera;
            Vector3 camTarget = (target.position - m_camera.transform.position).normalized;
            Quaternion camLookAt = Quaternion.LookRotation(camTarget);
            Vector3 camLookAtEuler = camLookAt.eulerAngles;
            Quaternion camRealTarget = Quaternion.Euler(camLookAtEuler.x, 0, 0);

        //---------------------------------------------

        //Transform ----------------------------------
            
            Quaternion trInitial = transform.rotation;
            Vector3 trTarget = (target.position - transform.position).normalized;
            Quaternion trLookAt = Quaternion.LookRotation(trTarget);
            Vector3 trLookAtEuler = trLookAt.eulerAngles;
            Quaternion trRealTarget = Quaternion.Euler(0, trLookAtEuler.y, 0);

        //-----------------------------------------------------------

        for (float i = 0; i < 0.3f; i+=Time.deltaTime)
        {
            float t = i / 0.3f; //Time
            
            transform.rotation = Quaternion.Slerp(trInitial, trRealTarget, t); //Transform
            yCurrentCamera = Mathf.Lerp(camInitial, camRealTarget.y, t); //Camera
            
            yield return null; //Return frame
        }

        transform.rotation = trRealTarget; //Transform
        yCurrentCamera = camRealTarget.y;
    }
}
