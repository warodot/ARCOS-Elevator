using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AbrirPuerta : MonoBehaviour
{
    [SerializeField] private GameObject[] Luces;

    [SerializeField] bool activated; 

    public UnityEvent openDoor;

    public UnityEvent closeDoor;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Verificacion()
    {
        for(int i = 0; i < Luces.Length; i++)
        {
            if(Luces[i].activeSelf == false)
            {
                activated = false;
                closeDoor?.Invoke(); break;

            }
            else
            {
                activated = true;
            }
        }

        if (activated == true)
        {
            openDoor?.Invoke();
        }
        else
        {
            closeDoor?.Invoke();
        }
    }
}
