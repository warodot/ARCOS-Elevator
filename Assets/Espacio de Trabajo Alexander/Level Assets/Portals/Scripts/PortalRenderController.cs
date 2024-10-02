using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalRenderController : MonoBehaviour
{
    void Start()
    {
        ResizeResolution ();
    }

    #region Portal Render

    #region Portal Render Variables

    [Header ("Portal Rendering Controls")]
    [Space (15f)]

    /// <summary>
    /// The camera used to create the image for the second portal.
    /// </summary>
    [Tooltip ("Designate portal A and portal B. This camera should be the one attached to portal A.")]
    public Camera portalCameraA;

    /// <summary>
    /// The camera used to create the image for the first portal.
    /// </summary>
    [Tooltip ("Designate portal A and portal B. This camera should be the one attached to portal B.")]
    public Camera portalCameraB;

    [Space (5f)]

    /// <summary>
    /// The material the portal camera A projects onto.
    /// </summary>
    [Tooltip ("Designate portal A and portal B. This material should be the one attached to portal A.")]
    public Material cameraMaterialA;

    /// <summary>
    /// The material the portal camera B projects onto.
    /// </summary>
    [Tooltip ("Designate portal A and portal B. This material should be the one attached to portal B.")]
    public Material cameraMaterialB;

    #endregion Portal Render Variables


    void ResizeResolution()
    {
        if (portalCameraB.targetTexture != null)
        {
            portalCameraB.targetTexture.Release();
        }

        portalCameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMaterialB.mainTexture = portalCameraB.targetTexture;

        if (portalCameraA.targetTexture != null)
        {
            portalCameraA.targetTexture.Release();
        }

        portalCameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        cameraMaterialA.mainTexture = portalCameraA.targetTexture;
    }

    #endregion Portal Render
}