using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleportManager : MonoBehaviour
{
    /// <summary>
    /// Detects how close to the portal the player was when teleported.
    /// </summary>
    public Vector3 previousOffsetFromPortal
    {
        get;
        set;
    }


    /// <summary>
    /// Registers which portal the player is coming from, to which portal they are headed, what their position was, and what their rotation was.
    /// </summary>
    /// <param name = "exportPortal"> The portal that the player is being teleported from. </param>
    /// <param name = "importPortal"> The portal that the player is being teleported to. </param>
    /// <param name = "position"> The transform.position data of the player while they are being teleported. </param>
    /// <param name="rotation"> The transform.rotation data of the player while they are being teleported. </param>
    public virtual void Teleport (Transform exportPortal, Transform importPortal, Vector3 position, Quaternion rotation)
    {
        transform.SetPositionAndRotation (position, rotation);
    }


    /// <summary>
    /// Called when the player first touches the import portal.
    /// </summary>
    public virtual void EnterPortalThreshold ()
    {

    }


    /// <summary>
    /// Called when the player is no longer touching the portal system after being teleported.
    /// </summary>
    public virtual void ExitPortalThreshold () 
    {

    }
}