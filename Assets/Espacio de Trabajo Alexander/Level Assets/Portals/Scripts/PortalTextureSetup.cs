using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTextureSetup : MonoBehaviour
{
    void Start()
    {
        TextureSetup();
    }

    #region Portal Texture Setup

    #region Portal Texture Setup Variables

    [Header("Portal Texture Setup Controls")]
    [Space(15f)]

    /// <summary>
    /// A reference to the camera attached to portal A.
    /// </summary>
    [SerializeField] private Camera cameraA;

    /// <summary>
    /// A reference to the camera attached to portal B.
    /// </summary>
    [SerializeField] private Camera cameraB;

    /// <summary>
    /// A reference to the material attached to portal A.
    /// </summary>
    [SerializeField] private Material materialA;

    /// <summary>
    /// A reference to the material attached to portal B.
    /// </summary>
    [SerializeField] private Material materialB;

    #endregion Portal Texture Setup Variables

    /// <summary>
    /// Configures the textures for the portals.
    /// </summary>
    void TextureSetup()
    {
        if (cameraB.targetTexture != null)
        {
            cameraB.targetTexture.Release();
        }

        cameraB.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialB.mainTexture = cameraB.targetTexture;


        if (cameraA.targetTexture != null)
        {
            cameraA.targetTexture.Release();
        }

        cameraA.targetTexture = new RenderTexture(Screen.width, Screen.height, 24);
        materialA.mainTexture = cameraA.targetTexture;
    }

    #endregion Portal Texture Setup

}