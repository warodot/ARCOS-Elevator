using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LucasRojo;

public class EnemyPool : MonoBehaviour
{
    
    [SerializeField] private Pool frontNormalEnemy;
    [SerializeField] private Pool backNormalEnemy;
    [SerializeField] private Pool rightFlyingEnemy;
    [SerializeField] private Pool leftFlyingEnemy;


    private void Awake()
    {
        frontNormalEnemy.Initialize();
        backNormalEnemy.Initialize();
        rightFlyingEnemy.Initialize();
        leftFlyingEnemy.Initialize();
    }
    
    private void OnDisable()
    {
        DisableAll();
        
    }
    private void Update()
    {
        // DEBUG!!!
        if (Input.GetKeyDown(KeyCode.Keypad1) && DebugManager.instance.debugMode)
        {
            GameManager.instance.roundIsActive = true;
            StartCoroutine(FrontSpawner(0));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0) && DebugManager.instance.debugMode)
        {
            StopAllCoroutines();
            DisableAll();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7) && DebugManager.instance.debugMode)
        {
            StartSpawn();
        }
    }
    public void StartSpawn()
    {
        if (GameManager.instance.round == 1)
        {
            StartCoroutine(FrontSpawner(1));
        }
        else if (GameManager.instance.round == 2)
        {
            StartCoroutine(FrontSpawner(0));
            StartCoroutine(BackSpawner(0.5f));
        }
        if (GameManager.instance.round == 3)
        {
            float randomValue = Random.value;

            if (randomValue <= 0.5f)
            {
                StartCoroutine(FrontSpawner(0));
            }
            else
            {
                StartCoroutine(BackSpawner(0));
            }
        }
    }
    #region Front Spawners
    IEnumerator FrontSpawner(float delay)
    {
        while (GameManager.instance.roundIsActive)
        {
            GameObject enemy = frontNormalEnemy.Get();
            yield return new WaitForSeconds(Random.Range(1f + delay, 3f + delay));

        }
    }
    #endregion

    #region Back Spawners
    IEnumerator BackSpawner(float delay)
    {
        while (GameManager.instance.roundIsActive)
        {
            GameObject enemy = frontNormalEnemy.Get();
            yield return new WaitForSeconds(Random.Range(1f + delay, 3f + delay));

        }
    }
    #endregion

    #region UpperLeft Spawners
    IEnumerator UpperLeftSpawnerFromFront(float delay)
    {
        yield return new WaitForSeconds(Random.Range(1f + delay, 3f + delay));
        GameObject enemy = rightFlyingEnemy.Get();
    }
    #endregion

    #region UpperRightSpawners
    IEnumerator UpperRightSpawnerFromFront(float delay)
    {
        yield return new WaitForSeconds(Random.Range(1f + delay, 3f + delay));
        GameObject enemy = rightFlyingEnemy.Get();
    }
    #endregion

    public void DisableAll()
    {
        frontNormalEnemy.DisableAll();
        backNormalEnemy.DisableAll();
        rightFlyingEnemy.DisableAll();
        leftFlyingEnemy.DisableAll();
    }
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
