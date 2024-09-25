using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_FurnitureDoor : MonoBehaviour, DH_IinteractableObject
{
    float initialXPosition;
    public float targetXPosition;

    void Start()
    {
        initialXPosition = transform.localPosition.x;
    }

    bool isOpening;
    void Update()
    {
        if (isOpen && !isOpening)
        {
            isOpening = true;
            StopAllCoroutines();
            StartCoroutine(DoorBeh(true));
        }
        else if (!isOpen && isOpening)
        {
            isOpening = false;
            StopAllCoroutines();
            StartCoroutine(DoorBeh(false));
        }
    }

    public bool isOpen;
    public void Interact()
    {
        isOpen = !isOpen;
    }

    IEnumerator DoorBeh(bool active)
    {
        Vector3 initial = transform.localPosition;
        Vector3 target = active ? new Vector3(targetXPosition, transform.localPosition.y, transform.localPosition.z) 
        : new Vector3(initialXPosition, transform.localPosition.y, transform.localPosition.z);
        
        for (float i = 0; i < 0.5f; i+=Time.deltaTime)
        {
            float t = i / 0.5f;
            transform.localPosition = Vector3.Lerp(initial, target, t);
            yield return null;
        }

        transform.localPosition = target;
    }
}
