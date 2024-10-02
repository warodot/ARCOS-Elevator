using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCameraController : MonoBehaviour
{
    void Awake ()
    {
        // localPortalCamera.enabled = false;
    }

    void Start()
    {
        
    }

    #region Portal Camera Controls

    #region Portal Camera Controls Parameters

    [Header("Portal Camera Controls")]
    [Space(15f)]

    /// <summary>
    /// A reference to the PortalCameraController script used by the destination portal.
    /// </summary>
    [SerializeField] private PortalCameraController destinationPortalController;

    /// <summary>
    /// References the virtual camera of the player. We'll be using it's transform.
    /// </summary>
    [SerializeField] private Camera playerCamera;

    /// <summary>
    /// A reference to the camera the local portal uses.
    /// </summary>
    [Tooltip ("Use the camera attached to the local portal.")]
    [SerializeField] private Camera localPortalCamera;

    /// <summary>
    /// A reference to the camera the destination portal uses.
    /// </summary>
    [Tooltip ("Use the camera attached to the destination portal.")]
    [SerializeField] private Camera destinationPortalCamera;

    /// <summary>
    /// References the portal the player wishes to travel from, and which the player camera is closer to.
    /// </summary>
    [SerializeField] private MeshRenderer localPortal;

    /// <summary>
    /// References the portal the player wishes to travel to, and which the player camera is farther from.
    /// </summary>
    [SerializeField] private MeshRenderer destinationPortal;

    #endregion Portal Camera Controls Parameters

    /// <summary>
    /// Controls the movements of the cameras.
    /// </summary>
    void CameraMovement()
    {
        /*FindOffset ();
        RotatePortalCamera();*/

        // Make the portal camera have he same position and rotation relative to it's assigned portal that the player camera has for it's own camera.
        var m = transform.localToWorldMatrix * destinationPortalController.transform.worldToLocalMatrix * playerCamera.transform.localToWorldMatrix;
        localPortalCamera.transform.SetPositionAndRotation (m.GetColumn (3), m.rotation);
    }


    /// <summary>
    /// Finds the offset between the player's camera and the starting portal, meant to help move the other cameras.
    /// </summary>
    void FindOffset ()
    {
        Vector3 playerOffsetFromPortal = playerCamera.transform.position - destinationPortal.transform.position;

        destinationPortalCamera.transform.position = localPortal.transform.position + playerOffsetFromPortal;
    }


    /// <summary>
    /// Syncronizes the rotation of the portal cameras with the player camera's rotation to help sell the illusion.
    /// </summary>
    void RotatePortalCamera ()
    {
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle (localPortal.transform.rotation, destinationPortal.transform.rotation);
        Quaternion portalRotationalDifferente = Quaternion.AngleAxis (angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraRotation = portalRotationalDifferente * playerCamera.transform.forward;

        destinationPortalCamera.transform.rotation = Quaternion.LookRotation (newCameraRotation, Vector3.up);
    }


    /// <summary>
    /// Determines the behaviour of the camera
    /// </summary>
    void RenderCamera ()
    {
        /*if (!CameraUtility.VisibleFromCamera (destinationPortal.localPortal, playerCamera))
        {
            return;
        }*/
    }

    #endregion Portal Camera Controls

    void Update ()
    {
        CameraMovement();
    }
}