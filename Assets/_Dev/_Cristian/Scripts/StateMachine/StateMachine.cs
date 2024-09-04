using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    BaseState currentState;

    private void Start()
    {
        currentState = GetInitialState();
    }

    private void Update()
    {
        if (currentState != null)
        {
            Debug.Log(currentState);
            currentState.UpdateLogic();
        }
    }

    private void LateUpdate()
    {
        if (currentState != null)
        {
            currentState.UpdatePhysics();
        }
    }

    public void ChangeState(BaseState newState)
    {
        currentState.Exit();

        currentState = newState;
        currentState.Enter();
    }


    public BaseState GetCurrentState()
    {
        return currentState;
    }
    protected virtual BaseState GetInitialState()
    {
        return null;
    }
}
