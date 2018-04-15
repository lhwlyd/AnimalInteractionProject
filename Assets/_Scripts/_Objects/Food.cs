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
                if (ba.Key.GetStateManager().GetHungerState() == AnimalState.TOO_FULL)
                {
                    StopEating(ba.Value, ba.Key);
                }

            }
        }
    }

    public void Eaten( float consumeRate, BaseAnimal consumer )
    {   
        this.consumeRate += consumeRate;
        beingEaten = true;
        eaters[consumer] = consumeRate;
    }

    public void StopEating( float consumeRate, BaseAnimal consumer) {
        this.consumeRate -= consumeRate;
        eaters.Remove(consumer);
        beingEaten = (eaters.Count > 0);
    }


}
