using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    [SerializeField] private bool activo = false;

    public Transform aim;

    [SerializeField] private float distanse;
    public LayerMask layer;

    public GameObject postesDeLuz;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits = Physics.RaycastAll(aim.position, transform.up, distanse, layer);

        Debug.DrawRay(aim.position, transform.up * distanse, Color.red);


        foreach (RaycastHit hit in hits)
        {

            
            if (hit.collider.gameObject.CompareTag("Poste"))
            {

               
                postesDeLuz.SetActive(true);
            }


        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("AAAAAAAAAA");
        if(other.gameObject.CompareTag("Poste"))
        {
            activo = true;

            if(activo == true)
                postesDeLuz.SetActive(true);
           
               
            
         
        }
        else
        {
            activo = false;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

       
    }
}
