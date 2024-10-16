using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    [SerializeField] GameObject linkedSpawner;
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            linkedSpawner.SetActive(true);
            Destroy(gameObject);
        }
    }
}
