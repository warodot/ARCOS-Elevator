using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    void Start()
    {
        movePlayer = false;
    }

    #region Player Mover

    #region Player Mover Variables

    [Header ("Player Mover Controls")]
    [Space (15f)]

    /// <summary>
    /// When true, moves the player to the bathroom in fase 4.
    /// </summary>
    public bool movePlayer;

    [Space (5f)]

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
        if (movePlayer == true)
        {
            playerObject.position = teleportLocationTransform.position;
            playerObject.rotation = teleportLocationTransform.rotation;
            movePlayer = false;
        }
    }

    #endregion Player Mover

    void Update()
    {
        TeleportPlayer ();
    }
}