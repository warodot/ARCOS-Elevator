using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnCameraVisibilityBehaviour : MonoBehaviour
{
    public UnityEvent onVisible;
    public UnityEvent onInvisible;

    /// <summary>
    /// OnBecameVisible is called when the renderer became visible by any camera.
    /// </summary>
    void OnBecameVisible()
    {
        onVisible?.Invoke();
    }

    /// <summary>
    /// OnBecameInvisible is called when the renderer is no longer visible by any camera.
    /// </summary>
    void OnBecameInvisible()
    {
        onInvisible?.Invoke();
    }
}
