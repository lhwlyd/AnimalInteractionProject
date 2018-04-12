using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    [Range(0, 100)]
    protected float foodLeft, consumeRate;

    protected bool beingEaten;
    protected List<BaseAnimal> eaters;

    private void Start()
    {
        eaters = new List<BaseAnimal>();
    }
    // Prolly need to disable this script when no animal is eating it
    private void Update()
    {
        if (beingEaten) {
            foodLeft -= consumeRate * Time.deltaTime;
        }
    }

    public void Eaten( float consumeRate, BaseAnimal consumer )
    {
        this.consumeRate += consumeRate;
        beingEaten = true;
        eaters.Add(consumer);
    }

    public void StopEating( float consumeRate, BaseAnimal consumer) {
        this.consumeRate -= consumeRate;
        eaters.Remove(consumer);
        beingEaten = (eaters.Count > 0);
    }


}
