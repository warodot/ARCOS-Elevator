using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pool
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private Transform parent;
    [SerializeField] private int limit;

    private List<GameObject> pool;

    public void Initialize()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < limit; i++)
        {
            GameObject instance = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
            instance.SetActive(false);

            pool.Add(instance);
        }
    }

    public GameObject Get()
    {
        foreach (GameObject instance in pool)
        {
            if (instance.activeSelf == false)
            {
                instance.transform.position = parent.position;
                instance.transform.rotation = parent.rotation;

                instance.SetActive(true);
                //Me gustaria que este objeto se mueva al parent al activarse usando el mismo metodo dentro del Instantiate de arriba
                return instance;
            }
        }
        GameObject newInstance = Object.Instantiate(prefab, parent.position, Quaternion.identity, parent);
        pool.Add(newInstance);
        return null;
    }
    public void DisableAll()
    {
        foreach (GameObject instance in pool)
        {
            if (instance.activeSelf)
            {
                instance.SetActive(false);
            }
        }
    }
}
