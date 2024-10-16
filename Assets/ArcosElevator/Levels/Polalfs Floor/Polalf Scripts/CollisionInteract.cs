using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CollisionInteract : MonoBehaviour
{
    public UnityEvent OnEnter;
   

    [SerializeField] private string tagToInteract;

    
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == tagToInteract)
        {
            if(collision.gameObject.tag == "Player") Debug.Log("ya estoda we");

            OnEnter.Invoke();
        }
    }
    
}
