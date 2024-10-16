using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCameraController : MonoBehaviour
{
    #region Portal Camera

    #region Portal Camera Variables

    [Header("Portal Camera Controls")]
    [Space(15f)]

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

    /// <summary>
    /// The limit of how many times a portal can be repeated in the render.
    /// </summary>
    [SerializeField] private int recursionLimit = 5;

    #endregion Portal Camera Variables

    /// <summary>
    /// Moves the camera to imitate the movement and rotation of the player for a better looking portal.
    /// </summary>
    void SyncCameraPosition()
    {
        // Change the position of the camera.
        Vector3 playerOffsetFromPortal = playerCamera.position - foreignPortal.position;
        localCamera.transform.position = localPortal.position + playerOffsetFromPortal;
        
        // Change the rotation of the camera.
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle(localPortal.rotation, foreignPortal.rotation);

        Quaternion portalRotationDifference = Quaternion.AngleAxis(angularDifferenceBetweenPortalRotations, Vector3.up);
        Vector3 newCameraDirection = portalRotationDifference * playerCamera.forward;
        localCamera.transform.rotation = Quaternion.LookRotation (newCameraDirection, Vector3.up);

        /*var m = localCamera.transform.localToWorldMatrix * foreignPortal.transform.worldToLocalMatrix * playerCamera.transform.localToWorldMatrix;
        localCamera.transform.SetPositionAndRotation (m.GetColumn (3), m.rotation);*/
    }

    #endregion Portal Camera

    void Update ()
    {
        SyncCameraPosition();
    }
}