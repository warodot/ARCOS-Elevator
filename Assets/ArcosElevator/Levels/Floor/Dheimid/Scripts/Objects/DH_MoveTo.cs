using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_MoveTo : MonoBehaviour, DH_IinteractableObject
{
    public Vector3 desiredPosition;
    public Vector3 desiredRotation;
    public LayerMask m_layer;
    public bool oneTime;

    public bool rotate;

    public void Interact()
    {
        StartCoroutine(MoveTo());
    }

    public void ChangeLayer() => gameObject.layer = LayerToInt(m_layer);

    int LayerToInt(LayerMask layerMask)
    {
        int layer = Mathf.RoundToInt(Mathf.Log(layerMask.value, 2));
        return layer;
    }

    IEnumerator MoveTo()
    {
        Vector3 initial = transform.localPosition;
        Vector3 target = desiredPosition;
        
        Quaternion initialRot = transform.localRotation;
        Quaternion targetRot = Quaternion.Euler(desiredRotation);

        for (float i = 0; i < 1; i+=Time.deltaTime)
        {
            float t = i / 1;
            transform.localPosition = Vector3.Lerp(initial, target, t);
            if (rotate) transform.localRotation = Quaternion.Lerp(initialRot, targetRot, t);
            yield return null;
        }

        transform.localPosition = desiredPosition;
        if (rotate) transform.localRotation = targetRot;

        if (oneTime) Destroy(this);
    }
}
