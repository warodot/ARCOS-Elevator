using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DH_IndicatorItem : MonoBehaviour
{
    TextMeshPro m_text;
    GameObject m_player;
    GameObject m_camera;
    public GameObject m_inificator;

    void Start()
    {
        m_text = GetComponent<TextMeshPro>();
        m_player = DH_Inventory.Instance.gameObject;
        m_camera = CinemachineShake.Instance.gameObject;

        m_text.text = transform.parent.gameObject.name;
    }

    float indicatorSize;
    float yPos;
    void Update()
    {
        float distance = Vector3.Distance(transform.parent.position, m_player.transform.position);

        yPos = distance;
        indicatorSize = distance;

        indicatorSize = Mathf.Clamp(indicatorSize, 1, 3);
        yPos = Mathf.Clamp(yPos, 0.3f, 1.5f);

        //Aplicamos
        m_text.transform.localScale = new Vector3(indicatorSize, indicatorSize, 1);
        transform.position = new Vector3(transform.position.x, yPos, transform.position.z);

        Vector3 lookAt = (m_camera.transform.position - transform.position).normalized;
        Quaternion qLookAt = Quaternion.LookRotation(-lookAt); //En negativo porque anda webiao el coso.
        Vector3 lookAEuler = qLookAt.eulerAngles;
        

        if (distance > 1)
        {
            //Look at camera -------------------------

                transform.rotation = Quaternion.Euler(lookAEuler);

            //--------------------------------
        }
        else
        {
            //Look at camera -------------------------
        
            transform.rotation = Quaternion.Euler(new Vector3(0, lookAEuler.y, lookAEuler.z));

            //--------------------------------
        }
    }
}
