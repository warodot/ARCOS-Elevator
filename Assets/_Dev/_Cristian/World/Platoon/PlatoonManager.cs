using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatoonManager : MonoBehaviour
{
    [SerializeField] GameObject machinegunnerPrefab, riflemanPrefab, submachinegunnerPrefab;
    [SerializeField] Transform attachedSpawnPoint;

    [SerializeField] float maxMG, maxRifle, maxSub;

    public List<EnemySM> platoonMembers;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
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

        yield break;
    }
}
