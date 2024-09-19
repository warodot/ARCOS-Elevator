using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowCast : MonoBehaviour
{
    public LayerMask groundLayer; 
    public float maxRayDistance = 10f; 
    public float rayOffsetHeight = 2f;

    private void Update()
    {
        Vector3 rayStartPosition = transform.position + Vector3.up * rayOffsetHeight;
        RaycastHit hit;

        if (Physics.Raycast(rayStartPosition, Vector3.down, out hit, maxRayDistance + rayOffsetHeight, groundLayer))
        {
            
            Vector3 groundedPosition = new Vector3(transform.position.x, hit.point.y + 0.05f, transform.position.z);
            transform.position = groundedPosition;
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 rayStartPosition = transform.position + Vector3.up * rayOffsetHeight;
        
        Gizmos.color = Color.red;
        Gizmos.DrawLine(rayStartPosition, rayStartPosition + Vector3.down * (maxRayDistance + rayOffsetHeight));
    }
}
