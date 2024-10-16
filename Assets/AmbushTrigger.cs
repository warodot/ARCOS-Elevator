using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushTrigger : AmbushController
{
    public GameObject wallToDeactivate, wallToActivate;

    protected override void StartAmbush(GameObject wallToDeactivate, GameObject wallToActivate)
    {
        base.StartAmbush(wallToDeactivate, wallToActivate);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartAmbush(this.wallToDeactivate, this.wallToActivate);
        }
    }
}
