using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerticalDoor : MonoBehaviour
{
    public Transform doorTransform;
    public float openHeight = 3f;
    public float speed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;

    void Start()
    {
        closedPosition = doorTransform.position;
        openPosition = new Vector3(closedPosition.x, closedPosition.y + openHeight, closedPosition.z);
    }
    public void OpenDoorNow()
    {
        StartCoroutine(OpenDoor());
    }
    IEnumerator OpenDoor()
    {
        float timeElapsed = 0f;

        while (timeElapsed < 1f)
        {
            doorTransform.position = Vector3.Lerp(closedPosition, openPosition, timeElapsed);
            timeElapsed += Time.deltaTime * speed;
            yield return null;
        }

        doorTransform.position = openPosition;
    }
}
