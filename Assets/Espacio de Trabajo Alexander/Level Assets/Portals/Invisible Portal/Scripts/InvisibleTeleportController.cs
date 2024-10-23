using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleTeleportController : MonoBehaviour
{
    void Start()
    {

    }

    #region Portal

    #region Portal Variables

    [Header("Portal Controls")]
    [Space(15f)]

    /// <summary>
    /// References the portal controller on the foreign portal.
    /// </summary>
    [SerializeField] private InvisibleTeleportController invisibleForeignPortal;

    #endregion Portal Variables

    #endregion Portal

    #region Teleportation

    #region Teleportation Variables

    [Space(15f)]
    [Header("Teleportation Controls")]
    [Space(15f)]

    /// <summary>
    /// A list keeping track of all things traveling through the portal.
    /// </summary>
    [SerializeField] private List<PortalTeleportManager> invisibleTrackedTravellers;

    #endregion Teleportation Variables

    /// <summary>
    /// Handles the data needed when a traveller enters a portal.
    /// </summary>
    /// <param name = "traveller"> The game object traveling through the portal system. </param>
    void OnTravellerEnterInvisiblePortal(PortalTeleportManager traveller)
    {
        if (!invisibleTrackedTravellers.Contains(traveller))
        {
            traveller.EnterPortalThreshold();
            traveller.previousOffsetFromPortal = traveller.transform.position - transform.position;
            invisibleTrackedTravellers.Add(traveller);
        }
    }


    /// <summary>
    /// Teleports a traveller from one portal to the other depending on certain conditions.
    /// </summary>
    void TeleportTravellerInvisible()
    {
        for (int i = 0; i < invisibleTrackedTravellers.Count; i++)
        {
            PortalTeleportManager traveller = invisibleTrackedTravellers[i];
            Transform travellerTransform = traveller.transform;

            Vector3 offsetFromPortal = travellerTransform.position - transform.position;

            // Data needed to check if the traveller is in front of or behind the portal.
            int portalSlide = System.Math.Sign(Vector3.Dot(offsetFromPortal, transform.forward)); // Vector might not be correct
            int portalSlideOld = System.Math.Sign(Vector3.Dot(traveller.previousOffsetFromPortal, transform.forward));

            // Teleport the traveller if it has crossed from one side of the portal to the other.
            if (portalSlide != portalSlideOld)
            {
                var m = invisibleForeignPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * travellerTransform.localToWorldMatrix;
                traveller.Teleport(transform, invisibleForeignPortal.transform, m.GetColumn(3), m.rotation);

                // Corrects for OnTriggerEnter/Exit's requirement on calculations during FixedUpdate
                invisibleForeignPortal.OnTravellerEnterInvisiblePortal(traveller);
                invisibleTrackedTravellers.RemoveAt(i);
                i--;
            }
            else
            {
                traveller.previousOffsetFromPortal = offsetFromPortal;
            }
        }
    }

    #endregion Teleportation

    #region Triggers

    void OnTriggerEnter(Collider other)
    {
        var traveller = other.GetComponent<PortalTeleportManager>();

        if (traveller)
        {
            OnTravellerEnterInvisiblePortal(traveller);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var traveller = other.GetComponent<PortalTeleportManager>();

        if (traveller && invisibleTrackedTravellers.Contains(traveller))
        {
            traveller.ExitPortalThreshold();
            invisibleTrackedTravellers.Remove(traveller);
        }
    }

    #endregion Triggers

    void Update()
    {

    }

    void LateUpdate()
    {
        TeleportTravellerInvisible();
    }
}