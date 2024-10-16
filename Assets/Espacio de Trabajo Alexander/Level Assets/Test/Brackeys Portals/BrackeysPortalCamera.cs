using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrackeysPortalCamera : MonoBehaviour
{
    #region Portal Camera

    #region Portal Camera Variables

    [Header ("Portal Camera Controls")]
    [Space (15f)]

    /// <summary>
    /// The transform of the player camera.
    /// </summary>
    [SerializeField] private Transform playerCamera;

    /// <summary>
    /// The transform of the portal the camera is attached to.
    /// </summary>
    [SerializeField] private Transform localPortal;

    /// <summary>
    /// The transform of the portal oposite of the one the camera is attached to.
    /// </summary>
    [SerializeField] private Transform foreignPortal;

    /// <summary>
    /// A reference to the camera attached to the local portal.
    /// </summary>
    [SerializeField] private GameObject localCamera;

    #endregion Portal Camera Variables

    /// <summary>
    /// Moves the camera to imitate the movement and rotation of the player for a better looking portal.
    /// </summary>
    void SyncCameraPosition ()
    {
        // Change the position of the camera.
        Vector3 playerOffsetFromPortal = playerCamera.position - foreignPortal.position;
        localCamera.transform.position = localPortal.position + playerOffsetFromPortal;

        // Change the rotation of the camera.
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle (localPortal.rotation, foreignPortal.rotation);

        Quaternion portalRotationDifference = Quaternion.AngleAxis (angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationDifference * playerCamera.forward;
        localCamera.transform.rotation = Quaternion.LookRotation (newCameraDirection, Vector3.up);
    }

    #endregion Portal Camera

    void Update()
    {
        SyncCameraPosition ();
    }
}