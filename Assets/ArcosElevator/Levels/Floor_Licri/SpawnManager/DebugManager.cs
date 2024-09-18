using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LucasRojo;

public class DebugManager : MonoBehaviour
{
    public static DebugManager instance;

    public bool debugMode = false;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L)) 
        {
            debugMode = !debugMode;
        }
        if (debugMode)
        {
            //MOSTRAR QUE ESTAMOS EN DEBUG DE ALGUNA FORMA, PROBABLEMENTE MOSTRAR UI CON UTILIDADES
        }
    }
}
