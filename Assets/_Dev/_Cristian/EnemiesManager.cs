using ECM2;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public static EnemiesManager instance;

    [SerializeField] List<EnemySM> activeEnemies = new();
    [SerializeField] float tacticsCooldown;
    [SerializeField] float secondsSincePlayerMoved;

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    public void AddEnemy(EnemySM enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void RemoveEnemy(EnemySM enemy)
    {
        activeEnemies.Remove(enemy);
    }

    private void Update()
    {
        CheckPlayerMove();
        TacticsCooldown();
    }


    void CheckPlayerMove()
    {
        if (MapPlayerPosManager.instance.GetPlayerRef().GetComponent<Character>().GetVelocity() == Vector3.zero)
        {
            secondsSincePlayerMoved = Time.deltaTime;
        }
        else
        {
            secondsSincePlayerMoved = 0;
        }
    }
    void TacticsCooldown()
    {
        tacticsCooldown -= Time.deltaTime;
        if (tacticsCooldown <= 0 && activeEnemies.Count != 0)
        {
            PickTactic();
        }
    }

    void PickTactic()
    {
        if (secondsSincePlayerMoved > 10 && activeEnemies.Count < 3)
        {
            PickEnemyClosestToPlayer();
        }

        var rand = Random.Range(0, 10);
        if (rand < 3 && activeEnemies.Count == 2)
        {
            for (int i = 0; i < activeEnemies.Count; i++)
            {
                if(activeEnemies[i].soldierClass == EnemySM.SoldierClass.Submachinegunner || 
                    activeEnemies[i].soldierClass == EnemySM.SoldierClass.Rifleman)
                {
                    activeEnemies[i].selectedTactic = 1;
                    activeEnemies[i].givenRole = 0;
                }
                if (activeEnemies[i].soldierClass == EnemySM.SoldierClass.MachineGunner)
                {
                    activeEnemies[i].selectedTactic = 1;
                    activeEnemies[i].givenRole = 1;
                    break;
                }
            }
        }
    }

    void PickEnemyClosestToPlayer()
    {
        var chosenEnemy = activeEnemies[0];
        var chosenDistance = Vector3.Distance(
            a: activeEnemies[0].transform.position,
            b: MapPlayerPosManager.instance.GetPlayerRef().transform.position);
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            var checkDistance = Vector3.Distance(
                a: activeEnemies[0].transform.position,
                b: MapPlayerPosManager.instance.GetPlayerRef().transform.position);

            if (checkDistance > chosenDistance)
            {
                chosenEnemy = activeEnemies[i];
            }
        }
        chosenEnemy.selectedTactic = 0;
        chosenEnemy.ChangeState(chosenEnemy.tacticsHubState);
    }

    public enum Tactics
    {
        None,
        Flanking,
        GrenadeThrowing,
        SuppresiveFiring,
        Rushing
    }
}
