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

    [Space]
    public AudioSource m_source;
    public AudioClip m_lockedSound;
    public AudioClip m_closeSound;
    public AudioClip m_openSound;
    public AudioClip m_lockDoor;

    //Private variables
    float targetRotation;
    [HideInInspector] public bool isOpen;

    public void Interact()
    {
        if (DH_Inventory.Instance.m_tools.Contains(m_key)) UnlockDoor();

        if (!isLocked)
        {
            isOpen = !isOpen;
            targetRotation = isOpen ? finalRotation : initialRotation;

            m_source.PlayOneShot(isOpen ? m_openSound : m_closeSound);

            StopAllCoroutines();
            StartCoroutine(DoorBehavior(duration, Quaternion.Euler(transform.eulerAngles.x, targetRotation, transform.eulerAngles.z)));
        } 
        else 
        {
            //Sonido de puerta cerrada
            m_source.PlayOneShot(m_lockedSound);
            m_lockedAction?.Invoke();
        }
    }

    public void LockDoor()
    {
        if (isOpen) Interact();
        isLocked = true;
        m_source.PlayOneShot(m_lockDoor);
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
