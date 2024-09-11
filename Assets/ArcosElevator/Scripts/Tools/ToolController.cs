using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolController : MonoBehaviour
{
    // I don't really recommend doing this too much, but I can't be bothered to think of a better way right now.
    public static ToolController Instance { get; private set; }
    public GameObject currentTool;

    public float followSpeed = 45f;
    public float verticalSineAmplitude = 0.05f;
    public float verticalSineFrequency = 12f;

    private Vector3 lastTargetPosition;
    private float sineTime;


    private void Awake()
    {
        Instance = this;
        lastTargetPosition = transform.position;
    }


    public void EquipTool(Item item)
    {
        if (currentTool != null)
        {
            Destroy(currentTool);
        }

        if (item.Prefab != null)
        {
            currentTool = Instantiate(item.Prefab);
            currentTool.transform.position = transform.position;
        }

    }

    private void LateUpdate()
    {
        if (currentTool != null)
        {
            currentTool.transform.rotation = Quaternion.Slerp(currentTool.transform.rotation, transform.rotation, 45f * Time.deltaTime);

            Vector3 targetMovement = transform.position - lastTargetPosition; // Calculate target movement
            float targetSpeed = targetMovement.magnitude; // Get the speed of the target

            if (targetSpeed > 0.01f) // If transform is moving
            {
                sineTime += Time.deltaTime * verticalSineFrequency; 

                float sineOffset = Mathf.Sin(sineTime) * verticalSineAmplitude;

                Vector3 desiredPosition = transform.position + new Vector3(0, sineOffset, 0);
                currentTool.transform.position = Vector3.Lerp(currentTool.transform.position, desiredPosition, followSpeed * Time.deltaTime);
            }
            else
            {
                currentTool.transform.position = Vector3.Lerp(currentTool.transform.position, transform.position, followSpeed * Time.deltaTime);
                sineTime = 0;
            }

            lastTargetPosition = transform.position;


        }

    }
}
