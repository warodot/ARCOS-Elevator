using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour
{
    public Light redLight;
    public bool greenLight;
    public AudioSource button;
    public AudioClip beep;

    // Start is called before the first frame update
    void Start()
    {
        greenLight = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Bullet"))
        {
            redLight.color = Color.green;
            greenLight = true;
            button.PlayOneShot(beep);

        }
    }
}
