using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    #region Player Mover

    #region Player Mover Variables

    [Header ("Player Mover Controls")]
    [Space (15f)]

    /// <summary>
    /// A reference to the player game object.
    /// </summary>
    [SerializeField] private Transform playerObject;

    /// <summary>
    /// The location that the player will be teleported to.
    /// </summary>
    [SerializeField] private Transform teleportLocationTransform;

    #endregion Player Mover Variables

    /// <summary>
    /// When called teleports the player to the new location.
    /// </summary>
    public void TeleportPlayer ()
    {
        playerObject.SetPositionAndRotation(teleportLocationTransform.position, teleportLocationTransform.rotation);
    }

    #endregion Player Mover
}