using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class lacerVerde : MonoBehaviour
{

    public GameObject Lazer;
    public GameObject luz1;
    public UnityEvent verifiqued;
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
        if (other.gameObject.CompareTag("LacerVerde"))
        {
            verifiqued?.Invoke();
            Lazer.SetActive(true);
            luz1.SetActive(true);
            Debug.Log("aaaaaaaa");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("LacerVerde"))
        {
            verifiqued?.Invoke();
            Lazer.SetActive(false);
            luz1.SetActive(false);
        }
    }
}
