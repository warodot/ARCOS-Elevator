using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Generator : MonoBehaviour
{
    public UnityEvent onEnable;
    private bool isActive;
    private void OnCollisionEnter(Collision other) 
    {
        if (!isActive)
        {
            onEnable.Invoke();
            isActive = true;
        }
    }
}
