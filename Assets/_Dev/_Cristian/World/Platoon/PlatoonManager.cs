using ECM2;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.AI.Navigation;
using UnityEngine;

public class PlatoonManager : MonoBehaviour
{

    public static PlatoonManager instance;
    //List
    [SerializeField] List<GameObject> enemiesToSpawn;
    
    //Spawn
    [SerializeField] List<EnemySM> platoonMembers;
    [SerializeField] float maxEnemies;

    //Tactics
    [SerializeField] float tacticCooldown;
    private EnemySM _selectedSoldier;
    private float _playerNoMovementWait;
    private bool _isPlayerStill;
    bool _canExecuteTactic;

    float _spawnCycles;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    private void OnEnable()
    {
        StartCoroutine(TacticManager());
    }

    private void Update()
    { 
        IsPlayerStill();
        if(Input.GetKeyDown(KeyCode.F))
        {
            RebakeNavmesh();
        }
    }

    void IsPlayerStill()
    {
        if (MapPlayerPosManager.instance.GetPlayerRef().GetComponent<Character>().GetMovementDirection() == Vector3.zero)
        {
            _playerNoMovementWait += Time.deltaTime;
            if (_playerNoMovementWait >= 3f)
            {
                _isPlayerStill = true;
            }
        }
        else
        {
            _isPlayerStill = false;
        }
    }


    public IEnumerator SpawnPlatoon(Transform spawnPoint)
    {
        for (int i = 0; i < maxEnemies; i++)
        {
            EnemySM enemySM = Instantiate(enemiesToSpawn[i], spawnPoint.position, Quaternion.identity, transform).GetComponent<EnemySM>();
            string newVal = _spawnCycles.ToString() + i.ToString();
            enemySM.internalID = newVal;
        }
        _spawnCycles++;
        yield break;
    }

    public IEnumerator TacticManager()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (_canExecuteTactic)
            {
                float rand = Random.Range(0, 100);
            }
        }
    }

    public IEnumerator TacticCooldown()
    {
        _canExecuteTactic = false;
        yield return new WaitForSeconds(tacticCooldown);
        _canExecuteTactic = true;
        yield break;
    }

    void RebakeNavmesh()
    {
        FindObjectOfType<NavMeshSurface>().RemoveData();
        FindObjectOfType<NavMeshSurface>().BuildNavMesh();
    }
    public void SelectSoldier()
    {

    }

    //Grenade Launching
    public void ExecuteTacticOne()
    {

    }

    //Suppresive Fire
    public void ExecuteTacticTwo()
    {

    }

    //Flanking
    public void ExecuteTacticThree()
    {

    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }
}
