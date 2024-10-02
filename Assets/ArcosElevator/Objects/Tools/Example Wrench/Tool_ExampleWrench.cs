using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_ExampleWrench : Tool
{
    
    [SerializeField] private LayerMask collisionLayers = 1;
    [SerializeField] private AudioClip repairSound;

    void Start()
    {
        // Use the AddToolFunction method to add a repair function
        AddToolFunction(KeyCode.Mouse0, Repair);

        // Add more functions as needed
        AddToolFunction(KeyCode.R, SecondaryRepair);
    }

    // Example method
    void Repair()
    {
        // Example behaviour
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10, collisionLayers))
        {
            Debug.Log("repaired " + hitInfo.collider.gameObject.name);
            PlaySound(repairSound);
        }
    }

    // Example method 2
    void SecondaryRepair()
    {
        Debug.Log("Secondary Repair action triggered");
    }
}
