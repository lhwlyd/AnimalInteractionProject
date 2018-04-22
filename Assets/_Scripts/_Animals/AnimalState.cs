using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/****** Use the StateMachine instead ********/
public class AnimalState : MonoBehaviour {
    [Range(0, 100)] public float hungerPoints;

    private int baseState,
        hungerState,
        movingState;

    /****** Base state ********/
    public readonly static int IDLE = 0;
    public readonly static int SLEEPNIG = 1;
    public readonly static int EATING = 2;
    public readonly static int WALKING = 3;

    public int GetBaseState()
    {
        return this.baseState;
    }

    public void SetBaseState(int state) {
        this.baseState = state;
    }


    /****** Hunger state ******/
    public readonly static int LOOKING_FOR_FOOD = 0;
    public readonly static int CASUALLY_EATING = 1;
    public readonly static int TOO_FULL = 2;

    public int GetHungerState() {
        if (hungerPoints <= 50) {
            this.hungerState = LOOKING_FOR_FOOD;
        }

        if (hungerPoints > 50 && hungerPoints <= 90) {
            this.hungerState = CASUALLY_EATING;
        }

        if (hungerPoints > 90) {
            this.hungerState = TOO_FULL;
        }

        return this.hungerState;
    }

    /****** Walking state ******/
    public readonly static int WANDERING = 0;
    public readonly static int WALKING_FOR_FOOD = 1;
    public readonly static int TIRED = 2;

    public int GetMovingState() {
        // For now it's always wandering
        return this.movingState;
    }

    public AnimalState() {
        this.hungerPoints = 100;
        this.hungerState = TOO_FULL;
        this.movingState = WANDERING;
    }

}
