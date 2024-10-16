using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorMover : MonoBehaviour
{
    #region Elevator Mover

    #region Elevator Mover Variables

    [Header ("Elevator Movement Controls")]
    [Space (15f)]

    /// <summary>
    /// A reference to the elevator object being moved.
    /// </summary>
    [SerializeField] private GameObject elevatorObject;

    /// <summary>
    /// A reference to the new location the elevator is being moved to.
    /// </summary>
    [SerializeField] private Transform elevatorNewTransform;

    /// <summary>
    /// A reference to the player collider.
    /// </summary>
    [SerializeField] private Collider playerCollider;

    #endregion Elevator Mover Variables

    #endregion Elevator Mover

    #region Triggers

    void OnTriggerExit(Collider other)
    {
        if (other == playerCollider)
        {
            elevatorObject.transform.position = elevatorNewTransform.position;
        }
    }

    #endregion Triggers
}