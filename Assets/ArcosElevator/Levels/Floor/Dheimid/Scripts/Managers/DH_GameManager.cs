using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum GameStates
{
    Gameplay,
    Dialogue,
    Interacting,
    Cinematic,
    Mirilla,
    UI
}

public class DH_GameManager : MonoBehaviour
{
    public static event Action<GameStates> StateAction;
    public GameStates m_gameState;
    static GameStates m_state;
    public static GameStates State
    {
        get => m_state;
        set
        {
            m_state = value;
            StateAction?.Invoke(m_state);
        }
    }

    void Start() => State = GameStates.Gameplay;

    void OnEnable() => StateAction += StateCharacter;

    void OnDisable() => StateAction -= StateCharacter;

    void StateCharacter(GameStates state)
    {
        //Debug del estado actual
        Debug.Log(state.ToString());
        m_gameState = state;

        //Gameplay
        switch(state)
        {
            case GameStates.Gameplay:
                Cursor.lockState = CursorLockMode.Locked;
                //Extras
                break;
            case GameStates.UI:
                Cursor.lockState = CursorLockMode.None;
                //Extras
                break;
        }
    }

    public void ComeToGameplay()
    {
        State = GameStates.Gameplay;
    }
}
