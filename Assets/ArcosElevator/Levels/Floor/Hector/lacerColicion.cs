using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lacerColicion : MonoBehaviour
{
    public GameObject Lazer;
    public GameObject luz1;
    public GameObject Luz2;
    public GameObject Luz3;
    public GameObject Luz4;
    public GameObject Luz5;
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
        if(other.gameObject.CompareTag("Lacer"))
        {
            Lazer.SetActive(true);
            luz1.SetActive(true);
            Luz2.SetActive(true);
            Luz3.SetActive(true);
            Luz4.SetActive(true);
            Luz5.SetActive(true);

            Debug.Log("aaaaaaaa");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Lacer"))
        {

            Lazer.SetActive(false);
            luz1.SetActive(false);
            Luz2.SetActive(false);
            Luz3.SetActive(false);
            Luz4.SetActive(false);
            Luz5.SetActive(false);
        }
    }
}
