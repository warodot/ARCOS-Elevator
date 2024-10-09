using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caja : MonoBehaviour
{
    bool isGet;
    [SerializeField] Rigidbody rb;
    [SerializeField] Collider collider;

    public void GetObj()
    {
        isGet = true;
        collider.enabled = false;
        rb.useGravity = false;
        rb.isKinematic = true;
        Baston.instance.GetBox(gameObject);
    }

    public void LeaveObj()
    {
        collider.enabled = true;
        rb.useGravity = true;
        rb.isKinematic = false;
    }

}
