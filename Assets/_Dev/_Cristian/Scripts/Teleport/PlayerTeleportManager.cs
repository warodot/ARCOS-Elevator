using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTeleportManager : MonoBehaviour
{
    [SerializeField] float cooldownTimerCurrent, cooldownTimerMax;
    [SerializeField] int currentLevel;
    [SerializeField] Transform playerTransformRef;

    private void Update()
    {
        cooldownTimerCurrent -= Time.deltaTime;

        if(cooldownTimerCurrent < 0)
        {
            TeleportPlayer();
        }
    }


    void TeleportPlayer()
    {
        if(Input.GetKeyDown(KeyCode.X) && currentLevel == 1)
        {
            TeleportDown();
        }
        else if(Input.GetKeyDown(KeyCode.X) && currentLevel == 0)
        {
            TeleportUp();
        }
    }


    void TeleportUp()
    {
        cooldownTimerCurrent = cooldownTimerMax;
        playerTransformRef.position = new Vector3(playerTransformRef.position.x, playerTransformRef.position.y + 500, playerTransformRef.position.z);
        currentLevel = 1;
    }

    void TeleportDown()
    {
        cooldownTimerCurrent = cooldownTimerMax;
        playerTransformRef.position = new Vector3(playerTransformRef.position.x, playerTransformRef.position.y - 500, playerTransformRef.position.z);
        currentLevel = 0;
    }
}
