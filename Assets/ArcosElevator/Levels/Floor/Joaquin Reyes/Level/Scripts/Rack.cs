using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rack : MonoBehaviour
{
    public Animator anim;
    public Animator rack;
    public AudioSource sound;
    public BoxCollider box;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Crowbar"))
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.2f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                rack.SetTrigger("Brake");
                box.enabled = false;
                sound.Play();
            }
        }
    }
}
