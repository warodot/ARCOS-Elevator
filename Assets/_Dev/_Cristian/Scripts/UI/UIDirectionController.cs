using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIDirectionController : MonoBehaviour
{
    [SerializeField] Transform playerTransformRef;
    [SerializeField] Transform exitTransformRef;
    [SerializeField] Transform sphereTransformRef;


    private void Update()
    {
        GetDirection();
    }

    void GetDirection()
    {
        if (playerTransformRef == null) return;
        Vector3 direction = Vector3.Normalize(exitTransformRef.position - playerTransformRef.position);
        Vector3 offset = playerTransformRef.position + 4f * direction.x * direction;
        Vector3 newPos = new(offset.x, 520, offset.z);

        sphereTransformRef.position = newPos;
    }
}
