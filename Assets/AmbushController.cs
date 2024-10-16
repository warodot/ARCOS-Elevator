using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmbushController : MonoBehaviour
{
    protected virtual void StartAmbush(GameObject wallToDeactivate, GameObject wallToActivate)
    {
        wallToDeactivate.SetActive(false);
        wallToActivate.SetActive(true);
    }
}
