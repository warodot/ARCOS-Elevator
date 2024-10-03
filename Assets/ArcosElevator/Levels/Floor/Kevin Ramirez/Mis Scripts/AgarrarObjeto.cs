using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgarrarObjeto : MonoBehaviour
{
    public Interactor interactorScript;

    void Start()
    {

    }

    void Update()
    {
        
    }

    public void AgarrarUnPersonaje(IInteractable personajeInteractuado)
    {
        Debug.Log("Esta detectando el input del jugador");
        interactorScript.GetInteractableLookedAt().GetComponent<ControllerIA>().agarradoPorElPlayer = true;
    }
}
