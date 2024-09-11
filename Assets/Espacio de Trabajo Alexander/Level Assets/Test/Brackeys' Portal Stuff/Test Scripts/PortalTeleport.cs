using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleport : MonoBehaviour
{
    void Start()
    {
        inPortal = false;
    }

    #region Portal Teleport

    #region Portal Teleport Variables

    [Header ("Teleport Controls")]
    [Space (15f)]

    /// <summary>
    /// The transform of the player.
    /// </summary>
    [SerializeField] private Transform playerTransform;

    /// <summary>
    /// The transform the player gets teletransported to.
    /// </summary>
    [SerializeField] private Transform reciever;

    /// <summary>
    /// Shows if the player is in the portal.
    /// </summary>
    [SerializeField] private bool inPortal;

    #endregion Portal Teleport Variables

    /// <summary>
    /// Handles the teleportation of the player.
    /// </summary>
    void Teleporting ()
    {
        if (inPortal)
        {
            Vector3 portalToPlayer = playerTransform.position - transform.position;
            float dotProduct = Vector3.Dot (transform.up, portalToPlayer);

            // If true, player has crossed the portal.
            if (dotProduct < 0f)
            {
                // Teleport
                float rotationDifference = -Quaternion.Angle (transform.rotation, reciever.rotation);
                rotationDifference += 180f;
                playerTransform.Rotate(Vector3.up, rotationDifference);

                Vector3 positionOffest = Quaternion.Euler (0f, rotationDifference, 0f) * portalToPlayer;
                playerTransform.position = reciever.position + positionOffest;

                inPortal = false;
            }
        }
    }

    #endregion Portal Teleport

    #region Triggers

    void OnTriggerEnter (Collider other)
    {
        if (other.tag == "Player")
        {
            inPortal = true;
        }
    }


    void OnTriggerExit (Collider other)
    {
        if (other.tag == "Player")
        {
            inPortal = false;
        }
    }

    #endregion Triggers

    void Update()
    {
        Teleporting();
    }
}