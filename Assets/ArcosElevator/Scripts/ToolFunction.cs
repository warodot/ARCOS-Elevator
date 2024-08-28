using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ToolFunction : MonoBehaviour
{
    
    private bool canHoldUse;
    private bool isUsing;
    private bool readyToUse;
    private int useLimit;
    private int usesRemaining;
    private float timeBetweenUses;
    private float useRange;

    public AudioClip useSFX;
    public AudioClip failedUseSFX;
    public AudioClip reachedLimitUseSFX;

    [SerializeField] private bool m_useMouseButton;
    [SerializeField] private int m_mouseButtonIndex;
    [SerializeField] private KeyCode m_keyboardKey;

    void Update()
    {
        ToolInput();
    }


    private void ToolInput()
    {
        if (canHoldUse)
        {
            isUsing = Input.GetKey(m_keyboardKey);
        }
        else
        {
            isUsing = Input.GetKeyDown(m_keyboardKey);
        }

        if (readyToUse && isUsing && (usesRemaining > 0 || usesRemaining <= -1))
        {
            UseTool();
        }
        else if (readyToUse && isUsing && usesRemaining <= 0)
        {/*
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                weaponAudioSource.PlayOneShot(reachedLimitUseSFX);
            }*/
        }
    }

    private void UseTool()
    {
        Debug.Log("Used tool");
    }

}
