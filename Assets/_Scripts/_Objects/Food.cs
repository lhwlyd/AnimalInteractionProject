using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Edible {

    protected override void Start()
    {
        base.Start();
        foodLeft = 100f;
    }

    public override void Consumed(BaseAnimal animal)
    {
        if (this.foodLeft > 0)
        {
            float update = Time.deltaTime * eaters[animal];
            this.foodLeft -= update;
            animal.UpdateHungerLevel(update);
            Debug.Log(this.foodLeft);
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
