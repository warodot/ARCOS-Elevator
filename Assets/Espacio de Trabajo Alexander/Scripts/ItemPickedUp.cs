using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class ItemPickedUp : MonoBehaviour
{
    void Start()
    {
        
    }

    #region Item Pickup

    #region Item Pickup Variables

    [Header ("Item Pickup Controls")]
    [Space (15f)]

    /// <summary>
    /// The object that the player is suposed to pick up to trigger the cutscene.
    /// </summary>
    [SerializeField] private GameObject pickUpObject;

    /// <summary>
    /// The timeline clip that plays after the item is picked up.
    /// </summary>
    [SerializeField] private PlayableDirector soberTimeline;

    #endregion Item Pickup Variables

    void ChangeToCinematic ()
    {
        if (pickUpObject.gameObject.activeInHierarchy == false)
        {
            soberTimeline.Play();
        }
    }

    #endregion Item Pickup

    void Update()
    {
        ChangeToCinematic ();
    }
}