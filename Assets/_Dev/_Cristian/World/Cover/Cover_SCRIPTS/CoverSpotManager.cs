using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoverSpotManager : MonoBehaviour
{
    public static CoverSpotManager instance;
    [SerializeField] List<CoverSpot> coverSpots = new();

    private void Awake()
    {
        if (instance != null && instance != this) Destroy(this);
        else instance = this;
    }

    public void AddCoverSpot(CoverSpot coverSpot)
    {
        coverSpots.Add(coverSpot);
    }

    public List<CoverSpot> GetCoverSpots()
    {
        return coverSpots;
    }
}
