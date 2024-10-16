using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    void Start()
    {
        
    }

    #region Portal

    #region Portal Variables

    [Header ("Portal Controls")]
    [Space (15f)]

    /// <summary>
    /// References the portal controller on the foreign portal.
    /// </summary>
    [SerializeField] private PortalController foreignPortal;

    /// <summary>
    /// A reference to the player's camera.
    /// </summary>
    [Tooltip ("Use the main camera that the player camera is attached to.")]
    [SerializeField] private Camera playerCamera;

    /// <summary>
    /// The camera attacked to the portal.
    /// </summary>
    [SerializeField] private Camera portalCamera;

    /// <summary>
    /// References the mesh renderer that renders the portal.
    /// </summary>
    [SerializeField] private MeshRenderer portalRenderer;

    #endregion Portal Variables

    #endregion Portal

    #region Portal Rendering

    #region Portal Rendering Variables

    /// <summary>
    /// 
    /// </summary>
    /// <param name = "pos">  </param>
    /// <returns>  </returns>
    int SideOfPortal (Vector3 pos)
    {
        return System.Math.Sign (Vector3.Dot (pos - transform.position, transform.forward));
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name = "posA">  </param>
    /// <param name = "posB">  </param>
    /// <returns> If true </returns>
    bool SameSideOfPortal (Vector3 posA, Vector3 posB)
    {
        return SideOfPortal (posA) == SideOfPortal (posB);
    }


    /// <summary>
    /// Sets the thickness of the portal screen so as not to clip with camera near plane when player goes through
    /// </summary>
    /// <param name = "viewPoint"> The perspective viewpoint of the player. </param>
    /// <returns> An updated screen thickness to avoid clipping. </returns>
    float ProtectScreenFromClipping(Vector3 viewPoint)
    {
        float halfHeight = playerCamera.nearClipPlane * Mathf.Tan(playerCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float halfWidth = halfHeight * playerCamera.aspect;
        float dstToNearClipPlaneCorner = new Vector3(halfWidth, halfHeight, playerCamera.nearClipPlane).magnitude;

        Transform screenT = portalRenderer.transform;
        bool camFacingSameDirAsPortal = Vector3.Dot(transform.forward, transform.position - viewPoint) > 0;
        screenT.localScale = new Vector3(screenT.localScale.x, screenT.localScale.y);
        screenT.localPosition = Vector3.forward * ((camFacingSameDirAsPortal) ? 0.5f : -0.5f);
        return 0f;
    }


    /// <summary>
    /// Tracks the position of the portal camera.
    /// </summary>
    Vector3 portalCamPos
    {
        get
        {
            return portalCamera.transform.position;
        }
    }

    #endregion Portal Rendering Variables

    /// <summary>
    /// 
    /// </summary>
    public void PostPortalRender()
    {
        ProtectScreenFromClipping (playerCamera.transform.position);
    }

    /// <summary>
    /// Handles render clipping to make the transition between portals feel more natural.
    /// </summary>
    void HandleClipping ()
    {
        float screenThickness = foreignPortal.ProtectScreenFromClipping (portalCamera.transform.position);
    }

    #endregion Portal Rendering

    #region Teleportation

    #region Teleportation Variables

    [Space (15f)]
    [Header ("Teleportation Controls")]
    [Space (15f)]

    /// <summary>
    /// A list keeping track of all things traveling through the portal.
    /// </summary>
    [SerializeField] private List <PortalTeleportManager> trackedTravellers;

    #endregion Teleportation Variables

    /// <summary>
    /// Handles the data needed when a traveller enters a portal.
    /// </summary>
    /// <param name = "traveller"> The game object traveling through the portal system. </param>
    void OnTravellerEnterPortal (PortalTeleportManager traveller)
    {
        if (!trackedTravellers.Contains (traveller))
        {
            traveller.EnterPortalThreshold ();
            traveller.previousOffsetFromPortal = traveller.transform.position - transform.position;
            trackedTravellers.Add (traveller);
        }
    }


    /// <summary>
    /// Teleports a traveller from one portal to the other depending on certain conditions.
    /// </summary>
    void TeleportTraveller ()
    {
        for (int i = 0; i < trackedTravellers.Count; i++)
        {
            PortalTeleportManager traveller = trackedTravellers[i];
            Transform travellerTransform = traveller.transform;

            Vector3 offsetFromPortal = travellerTransform.position - transform.position;

            // Data needed to check if the traveller is in front of or behind the portal.
            int portalSlide = System.Math.Sign (Vector3.Dot (offsetFromPortal, transform.forward)); // Vector might not be correct
            int portalSlideOld = System.Math.Sign (Vector3.Dot (traveller.previousOffsetFromPortal, transform.forward));

            // Teleport the traveller if it has crossed from one side of the portal to the other.
            if (portalSlide != portalSlideOld)
            {
                var m = foreignPortal.transform.localToWorldMatrix * transform.worldToLocalMatrix * travellerTransform.localToWorldMatrix;
                traveller.Teleport (transform, foreignPortal.transform, m.GetColumn(3), m.rotation);

                // Corrects for OnTriggerEnter/Exit's requirement on calculations during FixedUpdate
                foreignPortal.OnTravellerEnterPortal (traveller);
                trackedTravellers.RemoveAt (i);
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

    void OnTriggerEnter (Collider other)
    {
        var traveller = other.GetComponent<PortalTeleportManager> ();

        if (traveller)
        {
            OnTravellerEnterPortal (traveller);
        }
    }

    void OnTriggerExit (Collider other)
    {
        var traveller = other.GetComponent<PortalTeleportManager> ();

        if (traveller && trackedTravellers.Contains(traveller))
        {
            traveller.ExitPortalThreshold ();
            trackedTravellers.Remove (traveller);
        }
    }

    #endregion Triggers

    void Update()
    {
        
    }

    void LateUpdate()
    {
        TeleportTraveller ();
    }
}