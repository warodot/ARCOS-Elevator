using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSyncController : MonoBehaviour
{
    #region Door Sync Controls

    #region Door Sync Controls Parameters

    [Header ("Door Sync Controls")]
    [Space (15f)]

    /// <summary>
    /// A reference to the Door code that governs the animations for the first door.
    /// The first door is the one the player should encounter first.
    /// </summary>
    [SerializeField] private RotatingDoor doorControllerDoorOne;

    /// <summary>
    /// A reference to the Door code that governs the animations for the second door.
    /// The second door is the one the player should encounter second.
    /// </summary>
    [SerializeField] private RotatingDoor doorControllerDoorTwo;

    #endregion Door Sync Controls Parameters

    /// <summary>
    /// Makes sure both doors are in the same state.
    /// </summary>
    void SyncDoors ()
    {
        if (doorControllerDoorOne.isOpen() == true)
        {
            // Change the boolean for the second door.
        }

        if (doorControllerDoorOne.isOpen() == false)
        {
            // Change the boolean for the second door.
        }

        if (doorControllerDoorTwo.isOpen() == true)
        {
            // Change the boolean for the first door.
        }

        if (doorControllerDoorTwo.isOpen() == true)
        {
            // Change the boolean for the first door.
        }
    }

    #endregion Door Sync Controls

    void Update()
    {
        SyncDoors();
    }
}