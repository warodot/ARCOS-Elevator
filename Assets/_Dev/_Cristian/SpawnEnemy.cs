using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public List<GameObject> enemies;


    private void OnEnable()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            Vector3 newPos = new(
                x:transform.position.x + Random.Range(.5f, .5f),
                y: transform.position.y,
                z: transform.position.z + Random.Range(.5f, .5f));
            Instantiate(enemies[i], newPos, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
