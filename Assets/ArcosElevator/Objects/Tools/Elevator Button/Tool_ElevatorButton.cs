using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool_ElevatorButton : Tool
{
    [SerializeField] private LayerMask collisionLayers = 1;

    // Start is called before the first frame update
    void Start()
    {
        AddToolFunction(KeyCode.Mouse0, TryInsertButton);
    }

    void TryInsertButton()
    {
        Debug.Log("button");

        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, 10, collisionLayers))
        {
            Debug.Log("repaired " + hitInfo.collider.gameObject.name);
        }
    }
}
