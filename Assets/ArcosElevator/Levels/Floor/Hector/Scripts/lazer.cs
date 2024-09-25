using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lazer : MonoBehaviour
{
    [SerializeField] private bool activo = false;

    public GameObject postesDeLuz;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Poste"))
        {
            activo = true;

            if(activo == true)
            {
                postesDeLuz.SetActive(true);
            }
               
            
         
        }
        else
        {
            activo = false;
        }
    }
}
