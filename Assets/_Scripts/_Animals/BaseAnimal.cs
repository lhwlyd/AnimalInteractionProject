using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAnimal: MonoBehaviour {
    [Range(0, 100)] float agressionLevel,
        intimateLevel,
        intelligenceLevel,
        hungerLevel,
        thirstLevel,
        healthLevel,
        speed;
    
    public abstract void Move(Vector3 destination);

    public abstract void Eat(float foodPoints);

    public abstract void Drink(float waterPoints);

    public abstract void Injured(float damage);

} 
