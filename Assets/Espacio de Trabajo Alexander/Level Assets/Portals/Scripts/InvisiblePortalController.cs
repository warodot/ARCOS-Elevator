using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisiblePortalController : MonoBehaviour
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
    public PortalController foreignPortal;

    /// <summary>
    /// References the mesh renderer that renders the portal.
    /// </summary>
    [SerializeField] private MeshRenderer portalRenderer;

    #endregion Portal Variables

    #endregion Portal

    #region Teleportation

    #region Teleportation Variables

    /// <summary>
    /// A list keeping track of all things traveling through the portal.
    /// </summary>
    private List<PortalTeleportManager> trackedTravellers;

    #endregion Teleportation Variables

    /// <summary>
    /// Handles the data needed when a traveller enters a portal.
    /// </summary>
    /// <param name = "traveller"> The game object traveling through the portal system. </param>
    void OnTravellerEnterPortal(PortalTeleportManager traveller)
    {
        if (!trackedTravellers.Contains(traveller))
        {
            traveller.EnterPortalThreshold();
            traveller.previousOffsetFromPortal = traveller.transform.position - transform.position;
            trackedTravellers.Add(traveller);
        }
    }


    /// <summary>
    /// Teleports a traveller from one portal to the other depending on certain conditions.
    /// </summary>
    void TeleportTraveller()
    {
        for (int i = 0; i < trackedTravellers.Count; i++)
        {
            PortalTeleportManager traveller = trackedTravellers[i];
            Transform travellerTransform = traveller.transform;

            Vector3 offsetFromPortal = travellerTransform.position - transform.position;

            // Data needed to check if the traveller is in front of or behind the portal.
            int portalSlide = System.Math.Sign(Vector3.Dot(offsetFromPortal, transform.forward)); // Vector might not be correct
            int portalSlideOld = System.Math.Sign(Vector3.Dot(traveller.previousOffsetFromPortal, transform.forward));

            // Teleport the traveller if it has crossed from one side of the portal to the other.
            if (portalSlide != portalSlideOld)
            {
                var m = foreignPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * travellerTransform.localToWorldMatrix;
                traveller.Teleport(transform, foreignPortal.transform, m.GetColumn(3), m.rotation);

                // Corrects for OnTriggerEnter/Exit's requirement on calculations during FixedUpdate
                //foreignPortal.OnTravellerEnterPortal (traveller);
                trackedTravellers.RemoveAt(i);
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
            OnTravellerEnterPortal(traveller);
        }
    }

    void OnTriggerExit(Collider other)
    {
        var traveller = other.GetComponent<PortalTeleportManager>();

        if (traveller && trackedTravellers.Contains(traveller))
        {
            traveller.ExitPortalThreshold();
            trackedTravellers.Remove(traveller);
        }
    }

    #endregion Triggers

    void Update()
    {

    }

    void LateUpdate()
    {
        TeleportTraveller();
    }
}