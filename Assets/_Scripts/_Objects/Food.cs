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
        //consumer.SwitchToPreviousState();
    }

    public void Consumed(float rate) {
        if (this.foodLeft > 0)
        {
            this.foodLeft -= Time.deltaTime * rate;
            foreach (KeyValuePair<BaseAnimal, float> pair in eaters)
            {
                pair.Key.UpdateHungerLevel(Time.deltaTime * pair.Value);
            }
        }
        else {
            foreach (KeyValuePair<BaseAnimal, float> pair in eaters) {
                pair.Key.SwitchToPreviousState();
                Debug.Log(pair.Key.GetStateMachine().GetCurrentState().GetType());
            }

            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (!beingEaten)
            return;

        Consumed(consumeRate);
        beingEaten = eaters.Count > 0;
    }
}
