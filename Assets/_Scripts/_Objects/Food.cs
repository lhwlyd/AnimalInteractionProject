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

    public void Eaten(float consumingRate, BaseAnimal consumer )
    {   
        this.consumeRate += consumingRate;
        beingEaten = true;
        eaters[consumer] = consumeRate;
    }

    public void StopEating( BaseAnimal consumer) {
        Debug.Log("Animal :" + eaters[consumer] + " just stopped eating!");
        this.consumeRate -= eaters[consumer];
        eaters.Remove(consumer);
        beingEaten = (eaters.Count > 0);
        consumer.SwitchToPreviousState();
    }

    public void Consumed(float rate) {
        if (this.foodLeft > 0)
        {
            this.foodLeft -= Time.deltaTime * rate;
        }
        else {
            foreach (KeyValuePair<BaseAnimal, float> pair in eaters) {
                Debug.Log(pair.Key);
                StopEating(pair.Key);
            }
        }
    }
}
