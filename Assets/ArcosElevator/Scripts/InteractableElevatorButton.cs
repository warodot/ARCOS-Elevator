using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is unfinished
/// </summary>
public class InteractableElevatorButton : Interactable
{ 

    public string sceneName;


    public override void Interact()
    {
        base.Interact();

        StartCoroutine(LoadSceneASync(sceneName));
    }

    public override void LookedAt()
    {
        base.LookedAt();
    }

    public override void LookedAway()
    {
        base.LookedAway();
    }

    IEnumerator LoadSceneASync(string levelToLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(levelToLoad);
        
        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / 0.9f);
            Debug.Log(progressValue);
            yield return null;
        }
    }
}

