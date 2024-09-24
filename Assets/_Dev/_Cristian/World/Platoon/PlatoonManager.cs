using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlatoonManager : MonoBehaviour
{
    [SerializeField] GameObject machinegunnerPrefab, riflemanPrefab, submachinegunnerPrefab;
    [SerializeField] Transform attachedSpawnPoint;

    [SerializeField] float maxMG, maxRifle, maxSub;
    [SerializeField] float tacticCooldown;
    public List<EnemySM> platoonMembers;
    private EnemySM selectedSoldier;
    bool canExecuteTactic;

    private void OnEnable()
    {
        StartCoroutine(TacticManager());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(SpawnPlatoon());
        }
    }

    public IEnumerator SpawnPlatoon()
    {
        for (int i = 0; i < maxMG; i++)
        {
            platoonMembers.Add(Instantiate(machinegunnerPrefab, attachedSpawnPoint.position, Quaternion.identity).GetComponent<EnemySM>());
        }

        for (int i = 0; i < maxRifle; i++)
        {
            platoonMembers.Add(Instantiate(riflemanPrefab, attachedSpawnPoint.position, Quaternion.identity).GetComponent<EnemySM>());
            yield return new WaitForSeconds(.2f);

        }

        for (int i = 0; i < maxSub; i++)
        {
            platoonMembers.Add(Instantiate(submachinegunnerPrefab, attachedSpawnPoint.position, Quaternion.identity).GetComponent<EnemySM>());
            yield return new WaitForSeconds(.2f);
        }
        EnemyAIManager.instance.activeEnemies.AddRange(platoonMembers);
        yield break;
    }

    public IEnumerator TacticManager()
    {
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (canExecuteTactic)
            {
                float rand = Random.Range(0,100);
            }
        }
    }

    public IEnumerator TacticCooldown()
    {
        canExecuteTactic = false;
        yield return new WaitForSeconds(tacticCooldown);
        canExecuteTactic = true;
        yield break;
    }

    public void SelectSoldier()
    {

    }

    //Grenade Launching
    public void ExecuteTacticOne()
    {

    }

    //Specify Tactic Two
    public void ExecuteTacticTwo()
    {

    }

    //Specify Tactic Three
    public void ExecuteTacticThree()
    {

    }
}
