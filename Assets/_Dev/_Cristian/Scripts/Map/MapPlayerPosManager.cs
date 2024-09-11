using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerPosManager : MonoBehaviour
{
    public static MapPlayerPosManager instance;

    [SerializeField] GameObject playerRef;
    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    public GameObject GetPlayerRef()
    {
        return playerRef;
    }
}
