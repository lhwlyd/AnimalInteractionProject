using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Edible {

    protected override void Start()
    {
        base.Start();
        foodLeft = 50f;
    }

    public override void Consumed(BaseAnimal animal)
    {
        if (foodLeft > 0)
        {
            float update = Time.deltaTime * eaters[animal];
            foodLeft -= update;
            animal.UpdateHungerLevel(update);
            // Debug.Log(foodLeft);
        }
        else
        {
            // Debug.Log("food ran out");
            animal.SwitchToPreviousState();
            if (eaters.Count == 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
