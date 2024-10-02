using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    public Interactor interactorScript;
    public ControllerIA ControllerIAScript;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void AgarrarUnPersonaje(IInteractable personajeInteractuado)
    {
        Debug.Log("Esta detectando el input del jugador");
        ControllerIAScript.agarradoPorElPlayer = true;
    }
}
