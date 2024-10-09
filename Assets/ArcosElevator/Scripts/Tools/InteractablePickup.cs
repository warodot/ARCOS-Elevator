using System.Collections;
using System.Collections.Generic;
using Tellory.UI.RingMenu;
using UnityEngine;


public class InteractablePickup : Interactable
{ 

    public Item item;


    public override void Interact()
    {
        base.Interact();
        RingMenuManager.Instance.AddItem(item);
        gameObject.SetActive(false);
    }

    public override void LookedAt()
    {
        base.LookedAt();
    }

    public override void LookedAway()
    {
        base.LookedAway();
    }
}
