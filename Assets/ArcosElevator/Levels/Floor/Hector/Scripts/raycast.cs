using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class raycast : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;

    public LayerMask mask;

    public LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(pos1.position, pos2.position, 10000f, mask);

        lineRenderer.SetPosition(0, pos1.position);
        lineRenderer.SetPosition(1, pos2.position);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        pos2 = collision.transform;
    }
}
