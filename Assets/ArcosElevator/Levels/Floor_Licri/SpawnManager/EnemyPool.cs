using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LucasRojo;

public class EnemyPool : MonoBehaviour
{
    
    [SerializeField] private Pool normalEnemy;
    [SerializeField] private Pool flyingEnemy;

   
    private void Awake()
    {
        normalEnemy.Initialize();
        flyingEnemy.Initialize();
    }
    
    private void OnDisable()
    {
        normalEnemy.DisableAll();
        flyingEnemy.DisableAll();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1) && DebugManager.instance.debugMode)
        {
            StartCoroutine(FrontSpawnerFromLeft(0));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0) && DebugManager.instance.debugMode)
        {
            StopAllCoroutines();
            normalEnemy.DisableAll();
            flyingEnemy.DisableAll();
        }
    }
    public void StartSpawn()
    {
        if (GameManager.instance.round == 1)
        {
            StartCoroutine(FrontSpawnerFromLeft(1));
        }
        else if (GameManager.instance.round == 2)
        {
            StartCoroutine(FrontSpawnerFromLeft(0));
            StartCoroutine(BackSpawnerFromLeft(0.5f));
        }
        if (GameManager.instance.round == 3)
        {
            float randomValue = Random.value;

            if (randomValue <= 0.5f)
            {
                StartCoroutine(FrontSpawnerFromLeft(0));
            }
            else
            {
                StartCoroutine(BackSpawnerFromLeft(0));
            }
        }
    }
    #region Front Spawners
    IEnumerator FrontSpawnerFromLeft(float delay)
    {
        GameObject enemy = normalEnemy.Get();
        yield return new WaitForSeconds(Random.Range(1f + delay, 3f + delay));
    }
    #endregion

    #region Back Spawners
    IEnumerator BackSpawnerFromLeft(float delay)
    {
        yield return new WaitForSeconds(Random.Range(1f + delay, 3f + delay));
        GameObject enemy = normalEnemy.Get();
    }
    #endregion

    #region UpperLeft Spawners
    IEnumerator UpperLeftSpawnerFromFront(float delay)
    {
        yield return new WaitForSeconds(Random.Range(1f + delay, 3f + delay));
        GameObject enemy = flyingEnemy.Get();
    }
    #endregion

    #region UpperRightSpawners
    IEnumerator UpperRightSpawnerFromFront(float delay)
    {
        yield return new WaitForSeconds(Random.Range(1f + delay, 3f + delay));
        GameObject enemy = flyingEnemy.Get();
    }
    #endregion
    //IEnumerator BlockSpawner()
    //{
    //    if (level == 1)
    //    {
    //        while (true)
    //        {
    //            yield return new WaitForSeconds(Random.Range(0.5f, 1));
    //            float randomValue = Random.value;

    //            if (randomValue <= 0.4f) // 40%
    //            {
    //                GameObject bomb = bombBlocks.Get();
    //            }
    //            else // 60%
    //            {
    //                GameObject block = blocks.Get();
    //            }

    //        }
    //    }
    //    else if (level == 2)
    //    {
    //        while (true)
    //        {
    //            yield return new WaitForSeconds(Random.Range(1f, 1.5f));
    //            float randomValue = Random.value;

    //            if (randomValue <= 0.2f) // 20%
    //            {
    //                GameObject bomb = bombBlocks.Get();
    //            }
    //            else // 70%
    //            {
    //                GameObject block = blocks.Get();
    //            }

    //        }
    //    }

    //}
}
