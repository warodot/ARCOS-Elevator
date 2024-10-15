using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRotate : MonoBehaviour
{
    public Transform playerTransform;
    public float rotationSpeed = 5f; 

    public void LookAtPlayerOnYAxis()
    {
        
        StartCoroutine(RotateTowardsPlayer());
    }

    private IEnumerator RotateTowardsPlayer()
    {
        
        Vector3 direction = playerTransform.position - transform.position;
        direction.y = 0; 

        
        Quaternion targetRotation = Quaternion.LookRotation(direction);
       
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.1f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        transform.rotation = targetRotation;
    }
}
