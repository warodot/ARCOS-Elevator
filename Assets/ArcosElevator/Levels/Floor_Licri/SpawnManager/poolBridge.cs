using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poolBridge : MonoBehaviour
{
    [SerializeField] private Pool pool;

    private void Start() => pool.Initialize();
    public GameObject Get() => pool.Get();
}
