using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ECM2;

public class MoveCancel : MonoBehaviour
{
    public FirstPersonController FPC;
    public Character charScript;
    public Rigidbody playerRB;
    public GameObject PlayerParent;
    public GameObject targetParent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            SetPlayerPosToCenter();
        }
    }
    public void ToggleMove()
    {
        FPC.canMove = !FPC.canMove;

        if (FPC.canMove)
        {
            charScript.maxAcceleration = 20;
            playerRB.constraints = RigidbodyConstraints.None;
        }
        else
        {
            charScript.maxAcceleration = 0;
            playerRB.constraints = RigidbodyConstraints.FreezePosition;
            
            
        }
    }

    public void SetPlayerPosToCenter()
    {
        PlayerParent.transform.position = targetParent.transform.position;
    }
}
