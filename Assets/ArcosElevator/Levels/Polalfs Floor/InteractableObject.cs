using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfInteract { Grab, Input}
[RequireComponent(typeof(Outline))]
public class InteractableObject : Interactor, IInteractable 
{
    private Outline outline;

    [SerializeField] private TypeOfInteract typeOfInteract;


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
    public virtual void Interact()
    {
        
    }
    public virtual void LookedAt()
    {
        outline.enabled = true;
    }
    public virtual void LookedAway()
    {
        outline.enabled = false;
    }
}
