using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerPosManager : MonoBehaviour
{
    public static MapPlayerPosManager instance;

    [SerializeField] GameObject playerRef;
    [SerializeField] Transform coverRef;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    public GameObject GetPlayerRef()
    {
        return playerRef;
    }

    public Transform GetCoverRef()
    {
        return coverRef;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(coverRef.position, 15);
    }
}
