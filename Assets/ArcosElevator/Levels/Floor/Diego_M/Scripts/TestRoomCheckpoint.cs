using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRoomCheckpoint : MonoBehaviour
{
    public Transform[] checkpointPoints;
    public LayerMask checkpointLayer;
    public Vector3 boxSize = new Vector3(1, 1, 1);
    private Transform lastCheckpoint;

    void Update()
    {
        DetectCheckpoint();
    }

    void DetectCheckpoint()
    {
        foreach (Transform checkpoint in checkpointPoints)
        {
            Collider[] hits = Physics.OverlapBox(checkpoint.position, boxSize / 2, Quaternion.identity, checkpointLayer);

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Player"))
                {
                    lastCheckpoint = checkpoint;
                }
            }
        }
    }

    public void Respawn(GameObject player)
    {
        if (lastCheckpoint != null)
        {
            StartCoroutine(RespawnCoroutine(player));
        }
    }
    private IEnumerator RespawnCoroutine(GameObject player)
    {
        yield return new WaitForSeconds(1f);

        player.transform.position = lastCheckpoint.position;
    }

    private void OnDrawGizmos()
    {
        if (checkpointPoints != null)
        {
            Gizmos.color = Color.green;
            foreach (Transform checkpoint in checkpointPoints)
            {
                Gizmos.DrawWireCube(checkpoint.position, boxSize);
            }
        }
    }
}
