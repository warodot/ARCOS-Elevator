using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TestPortalCamera : MonoBehaviour
{
    void Start()
    {
        
    }

    #region Test Portal Camera

    #region Test Portal Camera Parameters

    [Header ("Portal Camera Controls")]
    [Space (15f)]

    /// <summary>
    /// References the virtual camera of the player. We'll be using it's transform.
    /// </summary>
    [SerializeField] private Camera playerCamera;

    /// <summary>
    /// References the camera attacked to the destination portal which renders what you can see through the portal.
    /// </summary>
    [SerializeField] private Camera portalCamera;

    /// <summary>
    /// References the portal the player wishes to travel from, and which the player camera is closer to.
    /// </summary>
    [SerializeField] private Transform startPortal;

    /// <summary>
    /// References the portal the player wishes to travel to, and which the camera is attached to for rendering.
    /// </summary>
    [SerializeField] private Transform destinationPortal;

    #endregion Test Portal Camera Parameters

    /// <summary>
    /// Finds the offset between the player's camera and the starting portal, meant to help move the other camera.
    /// </summary>
    void FindOffset ()
    {
        Vector3 playerOffsetFromPortal = playerCamera.transform.position - startPortal.transform.position;
        portalCamera.transform.position = destinationPortal.position + playerOffsetFromPortal;
    }


    /// <summary>
    /// Syncronizes the rotation of the portal camera with the player camera's rotation to help sell the illusion.
    /// </summary>
    void RotatePortalCamera ()
    {
        float angularDifferenceBetweenPortalRotations = Quaternion.Angle (destinationPortal.rotation, startPortal.rotation);
        Quaternion portalRotationalDifferente = Quaternion.AngleAxis (angularDifferenceBetweenPortalRotations, Vector3.up);
        
        Vector3 newCameraRotation = portalRotationalDifferente * playerCamera.transform.forward;

        portalCamera.transform.rotation = Quaternion.LookRotation (newCameraRotation, Vector3.up);
    }

    #endregion Test Portal Camera

    void Update()
    {
        FindOffset ();
        RotatePortalCamera ();
    }
}

// Based on Brackey's tutorial: https://www.youtube.com/watch?v=cuQao3hEKfs 