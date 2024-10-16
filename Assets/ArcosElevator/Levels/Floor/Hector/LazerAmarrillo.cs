using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LazerAmarrillo : MonoBehaviour
{
    public GameObject Lazer;
    public GameObject luz1;
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
        if (other.gameObject.CompareTag("LazerAmarrillo"))
        {
            Lazer.SetActive(true);
            luz1.SetActive(true);
           

            Debug.Log("aaaaaaaa");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LazerAmarrillo"))
        {

            Lazer.SetActive(false);
            luz1.SetActive(false);
        
        }
    }
}
