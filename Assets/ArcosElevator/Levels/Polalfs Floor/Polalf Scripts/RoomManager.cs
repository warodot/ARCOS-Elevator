using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{

    [SerializeField] private Door doorA, doorB;
    [SerializeField] private string openAnim;

    public void ActivateDoorA()
    {
        doorA.isOpen = true;
        CheckDoors();
    } 
    public void DeactivateDoorA()
    {
        doorA.isOpen = false;
    }

    public void ActivateDoorB ()
    {
        doorB.isOpen = true;
        CheckDoors();
    }

    
 public void DeactivateDoorB()
    {
        doorB.isOpen = false;
    }

    private void CheckDoors()
    {
        if(doorA.isOpen &&  doorB.isOpen)
        {
            doorA.GetComponent<Animator>().Play(openAnim);
            doorB.GetComponent<Animator>().Play(openAnim);
        }
    }
    public void OpenDoors()
    {
        doorA.GetComponent<Animator>().Play(openAnim);
        doorB.GetComponent<Animator>().Play(openAnim);
    }

}
