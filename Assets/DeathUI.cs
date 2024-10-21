using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUI : MonoBehaviour
{
    [SerializeField] GameObject deathUI1, deathUI2;

    public void Death()
    {
        deathUI1.SetActive(true);
        deathUI2.SetActive(true);
        MapPlayerPosManager.instance.GetPlayerRef().SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
