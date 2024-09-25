using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    // I don't really recommend doing this too much, but I can't be bothered to think of a better way right now.
    public static ToolController Instance { get; private set; }
    public GameObject CurrentTool;

    public float FollowSpeed = 45f;
    public float RotationSpeed = 45f;
    public float VerticalSineAmplitude = 0.05f;
    public float VerticalSineFrequency = 12f;

    private Vector3 lastTargetPosition;
    private float sineTime;


    private void Awake()
    {
        Instance = this;
        lastTargetPosition = transform.position;
    }


    public void EquipTool(Item item)
    {

        if (CurrentTool != null)
        {
            Destroy(CurrentTool);
        }

        if (item.Prefab != null)
        {
            CurrentTool = Instantiate(item.Prefab);
            CurrentTool.transform.position = transform.position;
            CurrentTool.transform.rotation = transform.rotation;
        }

    }

    private void LateUpdate()
    {
        if (CurrentTool != null)
        {
            CurrentTool.transform.rotation = Quaternion.Slerp(CurrentTool.transform.rotation, transform.rotation, RotationSpeed * Time.deltaTime);

            Vector3 targetMovement = transform.position - lastTargetPosition; // Calculate target movement
            float targetSpeed = targetMovement.magnitude; // Get the speed of the target

            if (targetSpeed > 0.001f) // If transform is moving
            {
                sineTime += Time.deltaTime * VerticalSineFrequency; 

                float sineOffset = Mathf.Sin(sineTime) * VerticalSineAmplitude;

                Vector3 desiredPosition = transform.position + new Vector3(0, sineOffset, 0);
                CurrentTool.transform.position = Vector3.Lerp(CurrentTool.transform.position, desiredPosition, FollowSpeed * Time.deltaTime);
            }
            else
            {
                CurrentTool.transform.position = Vector3.Lerp(CurrentTool.transform.position, transform.position, FollowSpeed * Time.deltaTime);
                sineTime = 0;
            }

        }
        lastTargetPosition = transform.position;

    }
}
