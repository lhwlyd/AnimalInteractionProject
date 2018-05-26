using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Edible : MonoBehaviour
{

    [SerializeField]
    [Range(0, 100)]
    protected float foodLeft, consumeRate;

    protected bool beingEaten;
    protected Dictionary<BaseAnimal, float> eaters;

    protected virtual void Start()
    {
        consumeRate = 0f;
        eaters = new Dictionary<BaseAnimal, float>();
    }

    public void Eaten(float consumingRate, BaseAnimal consumer)
    {
        this.consumeRate += consumingRate;
        beingEaten = true;
        if (!eaters.ContainsKey(consumer))
            eaters.Add(consumer, consumingRate);
    }

    public void StopEating(BaseAnimal consumer)
    {
        //this.consumeRate -= eaters[consumer];
        eaters.Remove(consumer);
        beingEaten = (eaters.Count > 0);
    }

    public virtual void Consumed(BaseAnimal animal)
    {
    }

}
