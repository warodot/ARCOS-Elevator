using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BoxSpawner : MonoBehaviour
{

    [SerializeField] private TMP_Text text;
    [SerializeField] private int spawnCount = 0;
    [SerializeField] private GameObject m_prefab;
    [SerializeField] private Transform m_spawnPos;
    private void Start()
    {
        text.text = spawnCount.ToString();
    }
    public void Spawn()
    {
        if (spawnCount == 0) return;
        Instantiate(m_prefab, m_spawnPos.position,Quaternion.identity);

        spawnCount--;
        text.text = spawnCount.ToString();  

        if(spawnCount <=0) m_prefab = null;

    }


}
