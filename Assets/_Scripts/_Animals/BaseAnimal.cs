﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAnimal: MonoBehaviour {

    [SerializeField]
    protected GameObject playerRef;

    [Range(0, 100)] protected float agressionLevel,
        intimateLevel,
        intelligenceLevel,
        hungerLevel,
        thirstLevel,
        healthLevel,
        speed;
    protected NavMeshAgent agent;

    // Nav mesh vars
    protected Vector3 lastAgentVelocity;
    protected NavMeshPath lastAgentPath;
    protected Vector3 lastAgentDestination;


    public abstract void Move(Vector3 destination);

    public abstract void Eat(float foodPoints);

    public abstract void Drink(float waterPoints);

    public abstract void Injured(float damage);

} 
