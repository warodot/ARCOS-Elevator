using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPlayerPosManager : MonoBehaviour
{
    [SerializeField] Transform playerTransformRef;
    [SerializeField] MeshRenderer mapMeshRef;
    [SerializeField] Material mapMaterial;

    private void Awake()
    {
        //mapMaterial = mapMeshRef.material;
    }

    private void Update()
    {
        //mapMaterial.SetVector("_Player_Position", playerTransformRef.position);
    }
}
