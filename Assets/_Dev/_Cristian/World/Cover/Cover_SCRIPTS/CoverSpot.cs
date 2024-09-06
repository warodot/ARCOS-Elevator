using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngineInternal;

public class CoverSpot : MonoBehaviour
{
    private void Start()
    {
        CoverSpotManager.instance.AddCoverSpot(this);
    }
}
