    using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CinematicTrigger : MonoBehaviour
{
    [SerializeField] GameObject cinematicObj;

    private void OnTriggerEnter(Collider other)
    {
        MapPlayerPosManager.instance.GetPlayerRef().SetActive(false);
        cinematicObj.SetActive(true);
        gameObject.SetActive(false);
    }
}
