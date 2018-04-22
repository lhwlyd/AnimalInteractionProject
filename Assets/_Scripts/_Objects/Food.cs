using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    [Range(0, 100)]
    protected float foodLeft, consumeRate;

    protected bool beingEaten;
    protected Dictionary<BaseAnimal, float> eaters;

    private void Start()
    {
        foodLeft = 100f;
        consumeRate = 0f;
        eaters = new Dictionary<BaseAnimal, float>();
    }
    // Prolly need to disable this script when no animal is eating it
    private void Update()
    {
        if (beingEaten) {
            foodLeft -= consumeRate * Time.deltaTime;
            foreach (KeyValuePair<BaseAnimal, float> ba in eaters)
            {
                /*
                if (ba.Key.GetStateManager().GetHungerState() == AnimalState.TOO_FULL)
                {
                    StopEating(ba.Key);
                }
                */

            }
        }
    }

    public void Eaten(KeyValuePair<float, BaseAnimal> tempStorage )
    {   
        this.consumeRate += tempStorage.Key;
        beingEaten = true;
        eaters[tempStorage.Value] = consumeRate;
    }

    public void StopEating( BaseAnimal consumer) {
        Debug.Log(eaters[consumer]);
        this.consumeRate -= eaters[consumer];
        eaters.Remove(consumer);
        beingEaten = (eaters.Count > 0);
    }


}
