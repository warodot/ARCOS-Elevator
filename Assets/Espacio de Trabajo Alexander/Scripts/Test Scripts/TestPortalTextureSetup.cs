using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestPortalTextureSetup : MonoBehaviour
{
    void Start()
    {
        if (portalCamera.targetTexture != null)
        {
            portalCamera.targetTexture.Release();
        }

        portalCamera.targetTexture = new RenderTexture (Screen.width, Screen.height, 24);
        cameraMaterialB.mainTexture = portalCamera.targetTexture;
    }

    #region Portal Resolution

    #region Portal Resolution Variables

    /// <summary>
    /// The camera used to create the image for the portal.
    /// </summary>
    public Camera portalCamera;

    /// <summary>
    /// The material the portal camera projects onto.
    /// </summary>
    public Material cameraMaterialB;

    #endregion Portal Resolution Variables

    #endregion Portal Resolution

    void Update()
    {
        
    }
}

// Based on Brackey's tutorial: https://www.youtube.com/watch?v=cuQao3hEKfs 