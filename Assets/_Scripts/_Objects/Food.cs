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
        Debug.Log(eaters[consumer]);
        this.consumeRate -= eaters[consumer];
        eaters.Remove(consumer);
        beingEaten = (eaters.Count > 0);
    }

    public void Consumed( float consumedAmount ) {
        this.foodLeft -= consumedAmount;
    }
}
