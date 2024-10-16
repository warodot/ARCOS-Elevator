using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateDoor : MonoBehaviour
{

    [SerializeField] bool activated;

    [SerializeField] GameObject[] activadores;

    [SerializeField] Puerta puerta;


    public void VerifiquedActived()
    {
        for (int i = 0; i < activadores.Length; i++)
        {
            if (activadores[i].activeSelf == false)
            {
                activated = false;
                activate(); break;
            }
            else
            {
                activated = true;
            }
        }

        activate();
    }


    void activate()
    {
        if (!activated)
        {
            puerta.CloseDoor();
        }
        else
        {
            puerta.OpenDoor();
        }
    }

}
