using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAnimal: MonoBehaviour {
    float agressionLevel;
    float intimateLevel;
    float intelligenceLevel;
    float hungerLevel;
    float thirstLevel;
    float healthLevel;
    float speed;
    
    public abstract void Move(Transform destination);

    public abstract void Eat(float foodPoints);

    public abstract void Drink(float waterPoints);

    public abstract void Injured(float damage);

} 
