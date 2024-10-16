using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objects : MonoBehaviour
{
    public GameObject pistolPrefab;
    public GameObject crowbarPrefab;
    public bool pistol;
    public bool pistolGet;
    public bool crowbar;
    public bool crowbarGet;
    public bool key;
    public RotatingDoor door;

    // Start is called before the first frame update
    void Start()
    {
        pistol = false;
        pistolGet = false;
        crowbarGet = false;
        key = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (key)
        {
            door.isLocked = false;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (pistol && pistolGet)
            {
                pistolPrefab.SetActive(false);
                pistol = false;
            }

            else if (!pistol && pistolGet)
            {
                pistolPrefab.SetActive(true);
                pistol = true;
            }

            if (crowbar)
            {
                crowbarPrefab.SetActive(false);
                crowbar = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if (crowbar)
            {
                crowbarPrefab.SetActive(false);
                crowbar = false;
            }

            else if (!crowbar && crowbarGet)
            {
                crowbarPrefab.SetActive(true);
                crowbar = true;
            }

            if (pistol)
            {
                pistolPrefab.SetActive(false);
                pistol = false;
            }
        }
    }

    public void Pistol()
    {
        pistol = true;
        pistolGet = true;
    }

    public void Crowbar()
    {
        crowbar = true;
        crowbarGet = true;
        pistol = false;
    }

    public void Key()
    {
        key = true;
    }
}
