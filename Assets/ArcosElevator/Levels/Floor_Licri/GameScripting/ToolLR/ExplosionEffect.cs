using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private void OnEnable()
    {
        Destroy(gameObject, 2f);
    }
    
}
