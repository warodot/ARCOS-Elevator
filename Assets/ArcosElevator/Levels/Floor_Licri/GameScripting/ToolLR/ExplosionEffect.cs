using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    private void OnEnable()
    {
        Invoke(nameof(Deactivate), 3f);
        //Destroy(gameObject, 2f);
    }
    private void Deactivate()
    {
        
        gameObject.SetActive(false);
    }

}
