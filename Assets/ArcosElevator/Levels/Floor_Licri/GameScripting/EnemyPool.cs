using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LucasRojo;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool instance;

    [SerializeField] private Pool frontNormalEnemy;
    [SerializeField] private Pool backNormalEnemy;
    [SerializeField] private Pool rightFlyingEnemy;
    [SerializeField] private Pool leftFlyingEnemy;
    [Header("Special spawns")]
    public GameObject specialPrefab;
    public Transform up;
    public Transform leftCorner;
    public Transform rightCorner;
    public Transform rightBackCorner;
    public Transform leftBackCorner;
    public bool specialSpawned;
    [Header("Booleans")]
    public bool frontIsSpawning = false;
    public bool backIsSpawning = false;
    public bool rightIsSpawning = false;
    public bool leftIsSpawning = false;
    [Header("Patter Delays")]
    public float rapidFireDelay = 0.6f;
    public float delayedDelay = 1.2f;



    private void Awake()
    {
        instance = this;

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
        if (Input.GetKeyDown(KeyCode.Keypad1) && DebugManager.instance.debugMode) //TEST DE RONDA
        {
            GameManager.instance.roundIsActive = true;
            StartCoroutine(FrontSpawnerMain(0));
            StartCoroutine(BackSpawnerMain(0));
            StartCoroutine(RightSpawnerMain(0));
            StartCoroutine(LeftSpawnerMain(0));
        }
        else if (Input.GetKeyDown(KeyCode.Keypad0) && DebugManager.instance.debugMode) //DESACTIVAR TODO
        {
            GameManager.instance.roundIsActive = false;
            StopAllCoroutines();
            DisableAll();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad7) && DebugManager.instance.debugMode) //TEST DE SPAWN REAL
        {
            StartSpawn();
        }
        else if (Input.GetKeyDown(KeyCode.Keypad5) && DebugManager.instance.debugMode) //TEST DE ENEMIGO ESPECIAL
        {
            SpawnSpecialEnemy();
        }
    }
    public void StartSpawn()
    {
        StopAllCoroutines();
        GameManager.instance.ToggleRound(true);

        if (GameManager.instance.round == 1)
        {
            StartCoroutine(FrontSpawnerMain(0));
        }
        else if (GameManager.instance.round == 2)
        {
    
            StartCoroutine(FrontSpawnerMain(1f));
            StartCoroutine(BackSpawnerMain(1f));
        }
        else if (GameManager.instance.round == 3)
        {
            StartCoroutine(FrontSpawnerMain(1f));
            StartCoroutine(BackSpawnerMain(1f));
            StartCoroutine(UpperSpawnerMain(0f));
        }
        else if (GameManager.instance.round == 4)
        {
            StartCoroutine(FrontSpawnerMain(0f));
            StartCoroutine(BackSpawnerMain(0f));
            StartCoroutine(UpperSpawnerMain(0f));
        }
        else if (GameManager.instance.round == 5)
        {
            float randomValue = Random.value;

            if (randomValue <= 0.5f)
            {
                StartCoroutine(FrontSpawnerMain(0));
            }
            else
            {
                StartCoroutine(BackSpawnerMain(0));
            }

            StartCoroutine(RightSpawnerMain(0));
            StartCoroutine(LeftSpawnerMain(0));
        }
    }
    #region //////////////////////Front Spawners//////////////////////////
    IEnumerator FrontSpawnerMain(float delay)
    {
        while (GameManager.instance.roundIsActive)
        {
            float randomValue = Random.value;
            Debug.Log("FrontSpawner ejecutó un ciclo con resultado: " + randomValue);
            if (randomValue <= 0.6f) // 60%
            {
                StartCoroutine(FrontSpawnerDelayed());
            }
            else
            {
                StartCoroutine(FrontSpawnerRapidFire());
            }

            yield return new WaitForSeconds(5f + delay);

        }
    }
    //PATTERNS
    IEnumerator FrontSpawnerRapidFire()
    {
        
        _ = frontNormalEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = frontNormalEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = frontNormalEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = frontNormalEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
    }
    IEnumerator FrontSpawnerDelayed()
    {
        _ = frontNormalEnemy.Get();
        yield return new WaitForSeconds(delayedDelay);
        _ = frontNormalEnemy.Get();
        yield return new WaitForSeconds(delayedDelay);
    }
    #endregion

    #region //////////////////////Back Spawners//////////////////////////
    IEnumerator BackSpawnerMain(float delay)
    {
        while (GameManager.instance.roundIsActive)
        {
            float randomValue = Random.value;
            Debug.Log("BackSpawner ejecutó un ciclo con resultado: " + randomValue);
            if (randomValue <= 0.6f) // 60%
            {
                StartCoroutine(BackSpawnerDelayed());
            }
            else
            {
                StartCoroutine(BackSpawnerRapidFire());
            }

            yield return new WaitForSeconds(5f + delay);

        }
    }
    //PATTERNS
    IEnumerator BackSpawnerRapidFire()
    {

        _ = backNormalEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = backNormalEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = backNormalEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = backNormalEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
    }
    IEnumerator BackSpawnerDelayed()
    {
        _ = backNormalEnemy.Get();
        yield return new WaitForSeconds(delayedDelay);
        _ = backNormalEnemy.Get();
        yield return new WaitForSeconds(delayedDelay);
    }
    #endregion

    #region //////////////////////Right Spawners//////////////////////////
    IEnumerator RightSpawnerMain(float delay)
    {
        while (GameManager.instance.roundIsActive)
        {
            float randomValue = Random.value;
            Debug.Log("RightSpawner ejecutó un ciclo con resultado: " + randomValue);
            if (randomValue <= 0.6f) // 60%
            {
                StartCoroutine(RightSpawnerDelayed());
            }
            else
            {
                StartCoroutine(RightSpawnerRapidFire());
            }

            yield return new WaitForSeconds(5f + delay);

        }
    }
    //PATTERNS
    IEnumerator RightSpawnerRapidFire()
    {

        _ = rightFlyingEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = rightFlyingEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = rightFlyingEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = rightFlyingEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
    }
    IEnumerator RightSpawnerDelayed()
    {
        _ = rightFlyingEnemy.Get();
        yield return new WaitForSeconds(delayedDelay);
        _ = rightFlyingEnemy.Get();
        yield return new WaitForSeconds(delayedDelay);
    }
    #endregion

    #region //////////////////////Left Spawners//////////////////////////
    IEnumerator LeftSpawnerMain(float delay)
    {
        while (GameManager.instance.roundIsActive)
        {
            float randomValue = Random.value;
            Debug.Log("LeftSpawner ejecutó un ciclo con resultado: " + randomValue);
            if (randomValue <= 0.6f) // 60%
            {
                StartCoroutine(LeftSpawnerDelayed());
            }
            else
            {
                StartCoroutine(LeftSpawnerRapidFire());
            }

            yield return new WaitForSeconds(5f + delay);

        }
    }
    //PATTERNS
    IEnumerator LeftSpawnerRapidFire()
    {

        _ = leftFlyingEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = leftFlyingEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = leftFlyingEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
        _ = leftFlyingEnemy.Get();
        yield return new WaitForSeconds(rapidFireDelay);
    }
    IEnumerator LeftSpawnerDelayed()
    {
        _ = leftFlyingEnemy.Get();
        yield return new WaitForSeconds(delayedDelay);
        _ = leftFlyingEnemy.Get();
        yield return new WaitForSeconds(delayedDelay);
    }
    #endregion

    #region //////////////////////Upper Spawners//////////////////////////
    IEnumerator UpperSpawnerMain(float delay)
    {
        while (GameManager.instance.roundIsActive)
        {
            float randomValue = Random.value;
            Debug.Log("UpperSpawner ejecutó un ciclo con resultado: " + randomValue);
            if (randomValue <= 0.4f) // 4%
            {
                _ = leftFlyingEnemy.Get();
            }
            else if (randomValue > 0.4f &&  randomValue < 0.8f) // 45%
            {
                _ = rightFlyingEnemy.Get();
            }
            else
            {
                {
                    SpawnSpecialEnemy();
                }
            }

            yield return new WaitForSeconds(Random.Range(2.5f, 4.5f));

        }
    }
    
    #endregion

    public void SpawnSpecialEnemy()
    {
        if (specialSpawned)
        {
            return;
        }
        else
        {
            specialSpawned = true;

            Transform[] spawnLocations = { up, leftCorner, rightCorner, rightBackCorner, leftBackCorner };

            int randomIndex = Random.Range(0, spawnLocations.Length);
            Transform selectedLocation = spawnLocations[randomIndex];

            Instantiate(specialPrefab, selectedLocation.position, selectedLocation.rotation);
        }
        
    }

    public void DisableAll()
    {
        frontNormalEnemy.DisableAll();
        backNormalEnemy.DisableAll();
        rightFlyingEnemy.DisableAll();
        leftFlyingEnemy.DisableAll();
        StopAllCoroutines();
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
