using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfInteract { Grab, Input, Key}
[RequireComponent(typeof(Outline))]
public class InteractableObject : MonoBehaviour
{
    private Outline outline;

    [SerializeField] private TypeOfInteract typeOfInteract;
    

    private void OnEnable()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public TypeOfInteract ShowType()
    {

        return typeOfInteract;
       
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
