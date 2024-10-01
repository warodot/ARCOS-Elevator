using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class MoveCancel : MonoBehaviour
{
    public FirstPersonController FPC;

    public void ToggleMove()
    {
        FPC.canMove = !FPC.canMove;
    }
}
