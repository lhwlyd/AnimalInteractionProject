using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Reference: https://www.youtube.com/watch?v=D6hAftj3AgM */
public class StateMachine {

    private IState currentlyRunningState;

    private IState previousState;

    public void ChangeState(IState newState) {
        if (currentlyRunningState != null) {
            currentlyRunningState.Exit();
            if (newState.GetType().Equals(currentlyRunningState.GetType()))
            {
                // Do nothing(just execute the current state), duplicate states
                ExecuteStateUpdate();
                return;
            }
        }
        previousState = currentlyRunningState;
        currentlyRunningState = newState;
        currentlyRunningState.Enter();
    }

    public void ExecuteStateUpdate() {
        if(currentlyRunningState != null)
            currentlyRunningState.Execute();
    }

    public void SwtichToPreviousState() {
        currentlyRunningState.Exit();
        Debug.Log(previousState);
        currentlyRunningState = previousState;
        currentlyRunningState.Enter();
    }

    public IState GetCurrentState() {
        return currentlyRunningState;
    }
}
