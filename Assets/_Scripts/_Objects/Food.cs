using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    [SerializeField]
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
        eaters.Add(consumer, consumingRate);
    }

    public void StopEating( BaseAnimal consumer) {
        //this.consumeRate -= eaters[consumer];
        eaters.Remove(consumer);
        beingEaten = (eaters.Count > 0);
    }

    public void Consumed(BaseAnimal animal) {
        if (this.foodLeft > 0)
        {
            float update = Time.deltaTime * eaters[animal];
            this.foodLeft -= update;
            animal.UpdateHungerLevel(update);
        }
        else {
            animal.SwitchToPreviousState();
            if(eaters.Count == 0){
                Destroy(this.gameObject);
            }
        }
    }

}
