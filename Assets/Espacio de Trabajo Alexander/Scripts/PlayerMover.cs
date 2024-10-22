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
    [SerializeField] private bool movePlayer;

    /// <summary>
    /// A reference to the player game object.
    /// </summary>
    [SerializeField] private GameObject playerObject;

    /// <summary>
    /// The location that the player will be teleported to.
    /// </summary>
    [SerializeField] private Transform teleportLocationTransform;

    #endregion Player Mover Variables

    /// <summary>
    /// When called teleports the player to the new location.
    /// </summary>
    void TeleportPlayer ()
    {
        if (movePlayer == true)
        {
            playerObject.transform.position = teleportLocationTransform.position;
            playerObject.transform.rotation = teleportLocationTransform.rotation;
        }
    }

    #endregion Player Mover

    void Update()
    {
        TeleportPlayer ();
    }
}