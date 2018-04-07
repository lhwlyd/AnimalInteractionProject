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
    
    public abstract void Move();

    public abstract void Eat();

    public abstract void Drink();

    public abstract void Injured(float damage);

} 
