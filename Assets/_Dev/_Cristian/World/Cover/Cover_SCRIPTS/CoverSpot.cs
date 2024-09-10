using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngineInternal;

public class CoverSpot : MonoBehaviour
{
    [SerializeField] bool canSeePlayer;

    private void Start()
    {
        CoverSpotManager.instance.AddCoverSpot(this);
    }

    private void FixedUpdate()
    {
        
    }


    void SendLine()
    {

    }
}
