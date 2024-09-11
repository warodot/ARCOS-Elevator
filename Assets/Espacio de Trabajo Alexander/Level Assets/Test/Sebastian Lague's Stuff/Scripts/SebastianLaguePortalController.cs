using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SebastianLaguePortalController : MonoBehaviour
{
    void Awake()
    {
        portalCamera = GetComponentInChildren<Camera>();
        portalCamera.enabled = false;
    }

    void Start()
    {
        
    }

    #region Portal Controls

    #region Portal Controls Variables

    [Header ("Portal Controls")]
    [Space (15f)]

    /// <summary>
    /// A reference to another portal's controller.
    /// </summary>
    [SerializeField] private SebastianLaguePortalController linkedPortal;

    /// <summary>
    /// A reference to the screen that renders the other side of the portal.
    /// </summary>
    [SerializeField] private MeshRenderer screen;

    /// <summary>
    /// A reference to the player's camera.
    /// Use the master camera with the cinemachine brain.
    /// </summary>
    [Tooltip ("Use the master camera with the cinemachine brain that the player camera is attached to.")]
    [SerializeField] private Camera playerCamera;

    /// <summary>
    /// A reference to the camera the portal uses.
    /// </summary>
    [SerializeField] private Camera portalCamera;


    /// <summary>
    /// The render texture that the camera is projecting onto the portal.
    /// </summary>
    private RenderTexture viewTexture;

    #endregion Portal Controls Variables

    /// <summary>
    /// Creates the view texture for the portal and adjusts it to the screen resolution.
    /// </summary>
    void CreateViewTexture ()
    {
        if (viewTexture == null || viewTexture.width != Screen.width || viewTexture.height != Screen.height)
        {
            if (viewTexture != null)
            {
                viewTexture.Release();
            }

            viewTexture = new RenderTexture (Screen.width, Screen.height, 0);

            // Render the view from the portal camera to the view texture.
            portalCamera.targetTexture = viewTexture;

            // Display the view texture on the screen of the linked portal.
            linkedPortal.screen.material.SetTexture ("_MainTex", viewTexture);
        }
    }


    /// <summary>
    /// Renders the portal.
    /// Called just before the player camera is rendered.
    /// </summary>
    public void Render ()
    {
        screen.enabled = false;
        CreateViewTexture ();

        // Make the portal camera have he same position and rotation relative to it's assigned portal that the player camera has for it's own camera.
        var m = transform.localToWorldMatrix * linkedPortal.transform.worldToLocalMatrix * playerCamera.transform.localToWorldMatrix;
        portalCamera.transform.SetPositionAndRotation (m.GetColumn (3), m.rotation);

        // Render the camera
        portalCamera.Render ();

        screen.enabled = true;
    }

    #endregion Portal Controls

    void Update()
    {
        
    }
}