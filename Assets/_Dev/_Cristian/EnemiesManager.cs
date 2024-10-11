using ECM2;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using TMPro;
using Unity.VisualScripting;
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

    void Update()
    {
        CheckPlayerMove();
        TacticsCooldown();
        ForceTactic();
    }

    void ForceTactic()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1)) //Supressive Fire
        {
            SuppresiveFire();
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ThrowGrenade();
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Flank();
        }
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
        if (tacticsCooldown <= 0 && activeEnemies.Count > 0)
        {
            PickTactic();
        }
    }

    void PickTactic()
    {
        var rand = Random.Range(0, 10);
        if (rand <= 3 && activeEnemies.Count > 1)
        {
            SuppresiveFire();
        }
        else if (rand > 3 && rand <= 8 && activeEnemies.Count != 0)
        {
            ThrowGrenade();
        }
        else if (rand > 8 && rand <= 10 && activeEnemies.Count > 2)
        {
            Flank();
        }

        tacticsCooldown = 60f;
    }

    void SuppresiveFire()
    {
        for (int i = 0; i < activeEnemies.Count; i++)
        {
            if (activeEnemies[i].soldierClass == EnemySM.SoldierClass.MachineGunner)
            {
                activeEnemies[i].selectedTactic = 1;
                activeEnemies[i].ChangeState(activeEnemies[i].tacticsHubState);
                break;
            }
        }
    }

    void ThrowGrenade()
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

    void Flank()
    {

    }
    public void AddEnemy(EnemySM enemy)
    {
        activeEnemies.Add(enemy);
    }

    public void RemoveEnemy(EnemySM enemy)
    {
        activeEnemies.Remove(enemy);
    }

}
