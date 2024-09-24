using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngineInternal;

public class CoverSpot : MonoBehaviour
{
    [SerializeField] bool canSeePlayer;
    [SerializeField] bool nearPlayer;
    [SerializeField] bool isPicked;

    private void Start()
    {
        CoverSpotManager.instance.AddCoverSpot(this);
    }

    private void FixedUpdate()
    {
        canSeePlayer = SendLine();
    }

    bool SendLine()
    {
        if(Physics.Linecast(transform.position, MapPlayerPosManager.instance.GetPlayerRef().transform.position, out RaycastHit hitInfo))
        {
            if(hitInfo.collider.gameObject.CompareTag("Player"))
            {
                return true;
            }
            else return false;
        }
        return false;
    }

    public bool GetCanSeePlayer()
    {
        return canSeePlayer;
    }

    public bool GetIsPicked()
    {
        return isPicked;
    }

    public void SetIsPicked(bool newBool)
    {
        isPicked = newBool;
    }
}
