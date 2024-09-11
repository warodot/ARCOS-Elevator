using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorSyncController : MonoBehaviour
{
    void Start()
    {
        
    }

    #region Door Sync Controls

    #region Door Sync Controls Parameters

    [Header ("Door Sync Controls")]
    [Space (15f)]

    /// <summary>
    /// A reference to the Door code that governs the animations for the first door.
    /// The first door is the one the player should encounter first.
    /// </summary>
    [SerializeField] private Door doorControllerDoorOne;

    /// <summary>
    /// A reference to the Door code that governs the animations for the second door.
    /// The second door is the one the player should encounter second.
    /// </summary>
    [SerializeField] private Door doorControllerDoorTwo;

    #endregion Door Sync Controls Parameters

    /// <summary>
    /// Makes sure both doors are in the same state.
    /// </summary>
    void SyncDoors ()
    {
        if (doorControllerDoorOne.isOpen == true)
        {
            doorControllerDoorTwo.Open();
        }

        if (doorControllerDoorOne.isOpen == false)
        {
            doorControllerDoorTwo.Close();
        }

        if (doorControllerDoorTwo.isOpen == true)
        {
            doorControllerDoorOne.Open();
        }

        if (doorControllerDoorTwo.isOpen == false)
        {
            doorControllerDoorOne.Close();
        }
    }

    #endregion Door Sync Controls

    void Update()
    {
        SyncDoors();
    }
}