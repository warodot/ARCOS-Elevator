using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DH_Door : MonoBehaviour, DH_IinteractableObject
{
    [Header("State door and key")]
    public bool isLocked;
    public bool isLOCAL;
    public UnityEvent m_lockedAction;
    public GameObject m_key;

    [Space]
    public float initialRotation;
    public float finalRotation;
    public float duration;

    float targetRotation;
    bool isOpen;

    public void Interact()
    {
        if (!isLocked)
        {
            isOpen = !isOpen;
            targetRotation = isOpen ? finalRotation : initialRotation;

            StopAllCoroutines();
            StartCoroutine(DoorBehavior(duration, Quaternion.Euler(transform.eulerAngles.x, targetRotation, transform.eulerAngles.z)));
        } 
        else m_lockedAction?.Invoke();
    }

    public void UnlockDoor() => isLocked = false;

    IEnumerator DoorBehavior(float time, Quaternion targetRot)
    {
        Quaternion startRotation = isLOCAL ? transform.localRotation : transform.rotation;
        Quaternion desiredRotation = targetRot;

        for (float i = 0; i < time; i += Time.deltaTime)
        {
            float t = i / time;

            if (isLOCAL) transform.localRotation = Quaternion.Slerp(startRotation, desiredRotation, t);
            else transform.rotation = Quaternion.Slerp(startRotation, desiredRotation, t);

            yield return null;
        }

        if (isLOCAL) transform.localRotation = desiredRotation;
        else transform.rotation = desiredRotation;
    }
}
