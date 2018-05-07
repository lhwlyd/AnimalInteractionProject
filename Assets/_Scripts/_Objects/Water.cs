using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Edible
{
    protected override void Start()
    {
        base.Start();
        foodLeft = 30f;
    }

    public override void Consumed(BaseAnimal animal)
    {
        if (this.foodLeft > 0)
        {
            float update = Time.deltaTime * eaters[animal];
            this.foodLeft -= update;
            animal.UpdateThirstLevel(update);
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
