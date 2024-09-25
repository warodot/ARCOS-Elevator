using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalController : MonoBehaviour
{
    void Awake ()
    {
        localPortalCamera = GetComponentInChildren<Camera>();
        localPortalCamera.enabled = false;
    }

    void Start ()
    {

    }

    #region Portal Resolution

    #region Portal Resolution Variables

    /// <summary>
    /// A reference to another portal's controller.
    /// </summary>
    [SerializeField] private PortalController foreignPortal;

    /// <summary>
    /// A reference to the camera the local portal uses.
    /// </summary>
    [SerializeField] private Camera localPortalCamera;

    /// <summary>
    /// The material that the local portal is using.
    /// </summary>
    [SerializeField] private Material localPortalMaterial;

    #endregion Portal Resolution Variables

    /// <summary>
    /// Adjusts the render texture to the size of the screen for a better image.
    /// </summary>
    void AdjustRenderTexture ()
    {
        if (foreignPortal.localPortalCamera.targetTexture != null)
        {
            foreignPortal.localPortalCamera.targetTexture.Release();
        }

        foreignPortal.localPortalCamera.targetTexture = new RenderTexture (Screen.width, Screen.height, 24);
    }


    /*/// <summary>
    /// Renders the portal.
    /// Called just before the player camera is rendered.
    /// </summary>
    void RenderPortal ()
    {

        localPortalScreen.enabled = false;
        CreateViewTexture();

        // Render the camera
        //localPortalCamera.Render();

        localPortalScreen.enabled = true;
    }*/

    #endregion Portal Resolution

    void Update ()
    {
        //RenderPortal ();
    }
}