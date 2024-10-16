using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldVFX : MonoBehaviour
{
    public Material material;
    float value;

    private void OnEnable()
    {
        material = new(material);
    }
    void Update()
    {
        value += Time.deltaTime;
        var scaledValue = Mathf.Clamp(value / 1, 0, 3);
        material.SetFloat("_VarDeltaTime", scaledValue);
    }

    private void Destroy()
    {
        Destroy(this);  
    }
}