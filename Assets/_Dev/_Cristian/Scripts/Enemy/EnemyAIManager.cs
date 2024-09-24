using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIManager : MonoBehaviour
{
    public static EnemyAIManager instance;

    public List<EnemySM> activeEnemies;

    public float maxActiveEnemies, currentActiveEnemies;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    private void LateUpdate()
    {
        if (activeEnemies.Count > 0)
        {
            SetActiveEnemies();
        }
    }


    void SetActiveEnemies()
    {
        if (currentActiveEnemies < maxActiveEnemies)
        {
            int rand = Random.Range(0, activeEnemies.Count);
            activeEnemies[rand].canAttack = true;
            currentActiveEnemies++;
        }
    }
}
