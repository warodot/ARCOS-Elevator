using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrosshairAnimation : MonoBehaviour
{
    public RectTransform targetRectTransform;
    public Vector3 startScale = new Vector3(1, 1, 1);
    public Vector3 endScale = new Vector3(1.2f, 1.2f, 1.2f);
    public float animationSpeed = 1f;
    private void Update()
    {
        float t = Mathf.PingPong(Time.time * animationSpeed, 1);

        targetRectTransform.localScale = Vector3.Lerp(startScale, endScale, t);
    }
}
