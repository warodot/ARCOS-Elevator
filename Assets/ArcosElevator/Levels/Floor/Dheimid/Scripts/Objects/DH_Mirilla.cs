using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DH_Mirilla : MonoBehaviour
{
    void OnEnable()
    {
        DH_GameManager.State = GameStates.Mirilla;
    }

    void Update()
    {
        if (DH_GameManager.State != GameStates.Mirilla) gameObject.SetActive(false);
    }
}
