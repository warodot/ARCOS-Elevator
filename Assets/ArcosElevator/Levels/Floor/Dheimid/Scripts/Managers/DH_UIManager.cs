using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_UIManager : MonoBehaviour
{
    public CanvasGroup m_centerPoint;
    public float m_duration;

    bool canOperate;

    void Update()
    {
        if (DH_Interact.m_isDetectingObject && !canOperate)
        {
            canOperate = true;
            StopAllCoroutines();
            StartCoroutine(DetectingBehavior(canOperate));
        }
        else if (!DH_Interact.m_isDetectingObject && canOperate)
        {
            canOperate = false;
            StopAllCoroutines();
            StartCoroutine(DetectingBehavior(canOperate));
        }
    }

    IEnumerator DetectingBehavior(bool isDetecting)
    {
        float initial = m_centerPoint.alpha;
        float target = isDetecting ? 1 : 0;

        for (float i = 0; i < m_duration; i+= Time.deltaTime)
        {
            float t = i / m_duration;
            m_centerPoint.alpha = Mathf.Lerp(initial, target, t);
            yield return null;
        }

        m_centerPoint.alpha = target;
    }
}
