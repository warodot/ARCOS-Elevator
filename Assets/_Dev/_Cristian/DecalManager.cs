using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DecalManager : MonoBehaviour
{
    [SerializeField] List<Sprite> bulletHoleSprites = new();
    [SerializeField] SpriteRenderer decalMaterial;

    void OnEnable()
    {
        var rand = Random.Range(0, bulletHoleSprites.Count);
        decalMaterial.sprite = bulletHoleSprites[rand];
    }
}
