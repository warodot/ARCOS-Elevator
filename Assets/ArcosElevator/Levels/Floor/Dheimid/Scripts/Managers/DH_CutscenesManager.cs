using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DH_CutscenesManager : MonoBehaviour
{
    public void StartCuscene(PlayableDirector director)
    {
        director.Play();
        DH_GameManager.State = GameStates.Cinematic;
    }
}
