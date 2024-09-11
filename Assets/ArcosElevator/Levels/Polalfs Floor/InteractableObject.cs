using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfInteract { Destroy, Grab, Input}
public class InteractableObject : MonoBehaviour
{
    public TypeOfInteract typeOfInteract;

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public TypeOfInteract ShowType()
    {
        return typeOfInteract;
    }
}
