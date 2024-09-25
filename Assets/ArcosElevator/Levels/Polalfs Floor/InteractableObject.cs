using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfInteract { Grab, Input}
[RequireComponent(typeof(Outline))]
public class InteractableObject : MonoBehaviour
{
    private Outline outline;

    [SerializeField] private TypeOfInteract typeOfInteract;
    public Transform offset;

    private void OnEnable()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public string ShowType()
    {
       
        if(typeOfInteract == TypeOfInteract.Grab) return "Guardar objeto";
        else return "Presionar botón";
        
       
    }
    public void LookedAt()
    {
        outline.enabled = true;
    }
    public void LookedAway()
    {
        outline.enabled = false;
    }
}
