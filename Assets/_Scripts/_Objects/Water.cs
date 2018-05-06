using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{

    [SerializeField]
    [Range(0, 100)]
    protected float waterLeft, consumeRate;

    protected bool beingEaten;
    protected Dictionary<BaseAnimal, float> eaters;

    private void Start()
    {
        waterLeft = 100f;
        consumeRate = 0f;
        eaters = new Dictionary<BaseAnimal, float>();
    }

    public void Eaten(float consumingRate, BaseAnimal consumer)
    {
        this.consumeRate += consumingRate;
        beingEaten = true;
        eaters.Add(consumer, consumingRate);
    }

    public void StopEating(BaseAnimal consumer)
    {
        //this.consumeRate -= eaters[consumer];
        eaters.Remove(consumer);
        beingEaten = (eaters.Count > 0);
    }

    public void Consumed(BaseAnimal animal)
    {
        if (this.waterLeft > 0)
        {
            float update = Time.deltaTime * eaters[animal];
            this.waterLeft -= update;
            animal.UpdateHungerLevel(update);
            Debug.Log(this.waterLeft);
        }
        else
        {
            Debug.Log("food ran out");
            animal.SwitchToPreviousState();
            if (eaters.Count == 0)
            {
                Destroy(this.gameObject);
            }
        }
    }

}
