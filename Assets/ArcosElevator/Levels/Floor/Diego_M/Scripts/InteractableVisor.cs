using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableVisor : Interactable
{
    public override void Interact()
    {
        base.Interact();
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
