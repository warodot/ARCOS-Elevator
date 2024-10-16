using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is unfinished
/// </summary>
public class InteractableElevatorButton : Interactable
{

    public string sceneName;

    public GameObject MeshGameObject;


    public override void Interact()
    {
        base.Interact();
        
        ElevatorManager.Instance.StartChangeLevelRoutine(sceneName);
        
    }

    public override void LookedAt()
    {
        base.LookedAt();
    }

    public override void LookedAway()
    {
        base.LookedAway();
    }

    /// <summary>
    /// Resets the position and rotation of the button and its mesh, along with activating the collision box for interaction
    /// </summary>
    public void ResetButton()
    {
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;
        MeshGameObject.transform.localPosition = Vector3.zero;
        MeshGameObject.transform.localRotation = Quaternion.identity;
        GetComponent<BoxCollider>().enabled = true;

    }
    
    public void SetLayerRecursively(int layerNumber)
    {
        foreach (Transform trans in GetComponentsInChildren<Transform>(true))
        {
            trans.gameObject.layer = layerNumber;
        }
    }
}

