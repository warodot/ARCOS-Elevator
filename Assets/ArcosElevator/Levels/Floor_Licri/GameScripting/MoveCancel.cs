using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class MoveCancel : MonoBehaviour
{
    public FirstPersonController FPC;
    public Rigidbody playerRB;
    public GameObject PlayerParent;
    public GameObject targetParent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SetPlayerPosToCenter();
        }
    }
    public void ToggleMove()
    {
        FPC.canMove = !FPC.canMove;

        if (FPC.canMove)
        {
            
            playerRB.constraints = RigidbodyConstraints.None;
        }
        else
        {
            
            playerRB.constraints = RigidbodyConstraints.FreezePosition;
        }
    }

    public void SetPlayerPosToCenter()
    {
        PlayerParent.transform.position = targetParent.transform.position;
    }
}
