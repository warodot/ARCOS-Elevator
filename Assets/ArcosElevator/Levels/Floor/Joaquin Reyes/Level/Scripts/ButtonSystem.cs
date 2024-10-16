using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSystem : MonoBehaviour
{
    public List<Buttons> buttons;
    public Animator door;
    public AudioSource open;
    public AudioClip opening;
    public bool openDoor;

    // Start is called before the first frame update
    void Start()
    {
        openDoor = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (buttons[0].greenLight && buttons[1].greenLight && buttons[2].greenLight)
        {
            door.Play("Open");
            openDoor = true;
            open.Play();
        }

        Open();
    }

    void Open()
    {
        if (openDoor)
        {
            
        }
    }
}
