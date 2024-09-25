using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalCameraController : MonoBehaviour
{
    void Start()
    {
        
    }

    #region Portal Camera Controls

    #region Portal Camera Controls Parameters

    [Header("Portal Camera Controls")]
    [Space(15f)]

    /// <summary>
    /// References the virtual camera of the player. We'll be using it's transform.
    /// </summary>
    [Tooltip ("Use the master camera with the cinemachine brain that the player camera is attached to.")]
    [SerializeField] private Camera playerCamera;

    /// <summary>
    /// References the camera attacked to the foreign portal which renders what you can see through the portal.
    /// </summary>
    [SerializeField] private Camera foreignPortalCamera;

    /// <summary>
    /// References the portal the player wishes to travel from, and which the player camera is closer to.
    /// </summary>
    [SerializeField] private Transform localPortal;

    /// <summary>
    /// References the portal the player wishes to travel to, and which the camera is attached to for rendering.
    /// </summary>
    [SerializeField] private Transform foreignPortal;

    #endregion Portal Camera Controls Parameters

    /// <summary>
    /// Finds the offset between the player's camera and the starting portal, meant to help move the other camera.
    /// </summary>
    void FindOffset()
    {
        Vector3 playerOffsetFromPortal = playerCamera.transform.position - localPortal.transform.position;
        foreignPortalCamera.transform.position = foreignPortal.position + playerOffsetFromPortal;
    }


    /// <summary>
    /// Syncronizes the rotation of the portal camera with the player camera's rotation to help sell the illusion.
    /// </summary>
    void RotatePortalCamera()
    {
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle (foreignPortal.rotation, localPortal.rotation);
        Quaternion portalRotationalDifferente = Quaternion.AngleAxis (angularDifferenceBetweenPortalRotations, Vector3.up);

        Vector3 newCameraRotation = portalRotationalDifferente * playerCamera.transform.forward;

        foreignPortalCamera.transform.rotation = Quaternion.LookRotation(newCameraRotation, Vector3.up);
    }

    #endregion Portal Camera Controls

    void Update()
    {
        FindOffset();
        RotatePortalCamera();
    }
}