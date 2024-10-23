using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fase2Trigger : MonoBehaviour
{
    void Start()
    {
        portalOne.SetActive (true);
        portalTwo.SetActive (false);
        trigger.SetActive (true);
    }

    #region Fase Two Trigger

    #region Fase Two Trigger Variables

    [SerializeField] private Collider playerCollider;

    [SerializeField] private GameObject portalOne;

    [SerializeField] private GameObject portalTwo;

    [SerializeField] private GameObject trigger;

    #endregion Fase Two Trigger Variables

    #endregion Fase Two Trigger

    #region Triggers

    void OnTriggerEnter (Collider other)
    {
        if (other = playerCollider)
        {
            portalOne.SetActive(false);
            portalTwo.SetActive(true);
            trigger.SetActive(false);
        }
    }

    #endregion Triggers

    void Update()
    {
        
    }
}