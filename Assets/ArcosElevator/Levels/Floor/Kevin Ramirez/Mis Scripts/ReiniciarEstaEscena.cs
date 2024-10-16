using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReiniciarEstaEscena : MonoBehaviour
{
    public string nombreDeEscena;

    void Update()
    {
        SceneManager.LoadScene(nombreDeEscena);
    }
}
