using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPortalTextureSetup : MonoBehaviour
{
    void Start()
    {
        if (portalCameraB.targetTexture != null)
        {
            portalCameraB.targetTexture.Release();
        }

        portalCameraB.targetTexture = new RenderTexture (Screen.width, Screen.height, 24);
        cameraMaterialB.mainTexture = portalCameraB.targetTexture;

        if (portalCameraA.targetTexture != null)
        {
            portalCameraA.targetTexture.Release();
        }

        portalCameraA.targetTexture = new RenderTexture (Screen.width, Screen.height, 24);
        cameraMaterialA.mainTexture = portalCameraA.targetTexture;
    }

    #region Portal Resolution

    #region Portal Resolution Variables

    /// <summary>
    /// The camera used to create the image for the first portal.
    /// </summary>
    public Camera portalCameraB;

    /// <summary>
    /// The camera used to create the image for the second portal.
    /// </summary>
    public Camera portalCameraA;

    /// <summary>
    /// The material the portal camera B projects onto.
    /// </summary>
    public Material cameraMaterialB;

    /// <summary>
    /// The material the portal camera A projects onto.
    /// </summary>
    public Material cameraMaterialA;

    #endregion Portal Resolution Variables

    #endregion Portal Resolution

    void Update()
    {
        
    }
}

// Based on Brackey's tutorial: https://www.youtube.com/watch?v=cuQao3hEKfs 