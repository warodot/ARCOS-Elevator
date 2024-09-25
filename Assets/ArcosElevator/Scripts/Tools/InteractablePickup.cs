using System.Collections;
using System.Collections.Generic;
using Tellory.UI.RingMenu;
using UnityEngine;


[RequireComponent(typeof(Outline))]
public class InteractablePickup : Interactable
{ 

    public Item item;

    private Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();
        outline.enabled = false;
    }

    public override void Interact()
    {
        base.Interact();
        RingMenuManager.Instance.AddItem(item);
        gameObject.SetActive(false);
    }

    public override void LookedAt()
    {
        base.LookedAt();
        outline.enabled = true;
    }

    public override void LookedAway()
    {
        base.LookedAway();
        outline.enabled = false;
    }
}
