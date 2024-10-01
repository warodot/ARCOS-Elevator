using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationResetter : MonoBehaviour
{

    Transform _initialTransform;
    private void OnEnable()
    {
        _initialTransform = transform;
    }
    private void OnDisable()
    {
        transform.position = _initialTransform.position;
        GetComponent<Animator>().SetBool("Aiming", false);
    }
}
