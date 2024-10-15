using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_WalkableZones : MonoBehaviour
{
    public List<Vector3> pointZones = new List<Vector3>();
    public List<Vector3> finalPostions = new List<Vector3>();
    
    void OnDrawGizmos()
    {
        if (pointZones.Count > 0)
        {
            for (int i = 0; i < pointZones.Count; i++)
            {
                Vector3 pointZone = transform.position + pointZones[i];
                Gizmos.DrawSphere(pointZone, 0.2f);

                for (int j = 0; j < finalPostions.Count; j++)
                {
                    finalPostions[i] = pointZone;
                }
            }
        }
    }
}
