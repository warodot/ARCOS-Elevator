using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyTimer : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 10f);
    }
}
