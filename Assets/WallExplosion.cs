using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WallExplosion : MonoBehaviour
{
    public List<GameObject> wallFragments = new();
    public float explosionForce;
    public Transform explosionArea;
    private void OnEnable()
    {
        for (int i = 0; i < wallFragments.Count; i++) {
            GameObject currentWall = wallFragments[i];
            currentWall.SetActive(true);
            currentWall.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, explosionArea.position, 10f);
        }
    }
}
