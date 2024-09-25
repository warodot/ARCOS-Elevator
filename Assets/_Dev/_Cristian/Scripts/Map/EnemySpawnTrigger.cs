using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnTrigger : MonoBehaviour
{
    [SerializeField] Transform attachedSpawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(PlatoonManager.instance.SpawnPlatoon(attachedSpawnPoint));
        }
        gameObject.SetActive(false);
    }
}
