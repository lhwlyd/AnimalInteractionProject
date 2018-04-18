using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseAnimal: MonoBehaviour {

    [SerializeField]
    protected GameObject playerRef;

    [Range(0, 100)] protected float agressionLevel,
        intimateLevel,
        intelligenceLevel,
        thirstLevel,
        healthLevel,
        speed;
    protected NavMeshAgent agent;

    // Nav mesh vars
    protected Vector3 lastAgentVelocity;
    protected NavMeshPath lastAgentPath;
    protected Vector3 lastAgentDestination;

    protected AnimalState stateManager;

    public abstract void Move(Vector3 destination);

    public abstract void Eat(float foodPoints, Collider food);

    public abstract void Drink(float waterPoints);

    public abstract void Injured(float damage);


    public AnimalState GetStateManager() {
        return stateManager;
    }

    public void RecordAgentState(ref NavMeshAgent agent) {
        lastAgentVelocity = agent.velocity;
        lastAgentPath = agent.path;
        lastAgentDestination = agent.destination;
    }

    public void RestoreAgentState(ref NavMeshAgent agent) {
        agent.SetDestination(lastAgentDestination);
        agent.velocity = lastAgentVelocity;
    }
} 
