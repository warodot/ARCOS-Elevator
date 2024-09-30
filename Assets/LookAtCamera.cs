using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    void Update()
    {
        Vector3 cameraPosition = Camera.main.transform.position;

        transform.LookAt(cameraPosition);

        Vector3 eulerAngles = transform.eulerAngles;
        eulerAngles.x = 0; 
        transform.eulerAngles = eulerAngles;
    }
}
