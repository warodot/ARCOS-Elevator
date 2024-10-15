using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta : MonoBehaviour
{
    [SerializeField] Vector3 closePosition;
    [SerializeField] Vector3 openPosition;
    public void OpenDoor()
    {
        StartCoroutine(OpenLerp());
    }

    public IEnumerator OpenLerp()
    {
        for(float i =0; i < 1; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, openPosition, i/1);
            yield return null;
        }
    }

    public void CloseDoor()
    {
        StartCoroutine (CloseLerp());
    }

    public IEnumerator CloseLerp()
    {
        for (float i = 0; i < 1; i += Time.deltaTime)
        {
            transform.position = Vector3.Lerp(transform.position, closePosition, i / 1);
            yield return null;
        }
    }
}
