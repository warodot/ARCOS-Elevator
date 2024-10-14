using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldVFX : MonoBehaviour
{
    public Material material;
    float value;
    void Update()
    {
        value += Time.deltaTime;
        var scaledValue = Mathf.Clamp(value / 1, 0, 3);
        material.SetFloat("_VarDeltaTime", scaledValue);
        material.SetFloat("_Alpha", scaledValue * -1);
    }
}