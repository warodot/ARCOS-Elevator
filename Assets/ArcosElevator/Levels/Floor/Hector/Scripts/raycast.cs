using ECM2;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    public Transform pos1;
    //public Transform pos2;

    public LayerMask mask;

    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Physics.Raycast(pos1.position, transform.up, out RaycastHit hit, Mathf.Infinity, mask))
        {
            lineRenderer.SetPosition(0, pos1.position);
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(0, pos1.position);
            lineRenderer.SetPosition(1, pos1.position);
        }

    }

    private void OnDrawGizmosSelected()
    {
        Debug.DrawRay(pos1.position, transform.up);
    }
}
