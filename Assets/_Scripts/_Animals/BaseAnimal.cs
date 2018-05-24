using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseAnimal : MonoBehaviour {

    [SerializeField]
    protected GameObject playerRef;
    [SerializeField]
    protected LayerMask foodItemsLayer;
    [SerializeField]
    protected LayerMask waterItemsLayer;
    [SerializeField]
    protected float foodDetectRange;

    [SerializeField]
    [Range(0, 100)] protected float aggressionLevel,
        hungerLevel,
        intimateLevel,
        intelligenceLevel,
        thirstLevel,
        healthLevel,
        energyLevel, // is the animal tired or not
        foodConsumingRate,
        waterConsumingRate,
        speed;
    protected bool isBusy;
    public enum BusyType
    {
        Grabbed,
        Hurt,
        Ingesting,
        SearchingForResource,
        Resting
    };

    protected NavMeshAgent agent;

    // Nav mesh vars
    protected Vector3 lastAgentVelocity;
    protected NavMeshPath lastAgentPath;
    protected Vector3 lastAgentDestination;

    //protected AnimalState stateManager;
    protected StateMachine stateMachine = new StateMachine();

    public abstract void Move(Vector3 destination);

    public abstract void Eat(float foodPoints, Collider food);

    public abstract void Drink(float waterPoints);

    public abstract void Injured(float damage);

    public void RecordAgentState(ref NavMeshAgent agent) {
        lastAgentVelocity = agent.velocity;
        lastAgentPath = agent.path;
        lastAgentDestination = agent.destination;
    }

    public void RestoreAgentState(ref NavMeshAgent agent) {
        agent.SetDestination(lastAgentDestination);
        agent.velocity = lastAgentVelocity;
    }

    public float GetHungerLevel() {
        return hungerLevel;
    }

    public void UpdateHungerLevel(float update) {
        hungerLevel += update;
    }

    public float GetThirstLevel()
    {
        return thirstLevel;
    }

    public void UpdateThirstLevel(float update)
    {
        thirstLevel += update;
    }

    public void UpdateEnergyLevel(float update) {
        energyLevel += update;
    }

    public float GetEnergyLevel() {
        return energyLevel;
    }

    public void UpdateIntimateLevel(float update) {
        intimateLevel += update;
    }

    public float GetIntimateLevel() {
        return intimateLevel;
    }

    public void UpdateAggresionLevel(float update) {
        aggressionLevel += update;
    }

    public float GetAggression() {
        return aggressionLevel;
    }

    public void SetBusy(BusyType busyType) {
        isBusy = true;
        switch (busyType) {
            case BusyType.Grabbed:
                RecordAgentState(ref agent);
                stateMachine.ChangeState(new Grabbed(agent));
            break;

            case BusyType.Hurt:
                break;

            case BusyType.Ingesting:
                break;
        }

    }

    public void ExitBusy(){
        isBusy = false;
    }

    public bool IsBusy() {
        return isBusy;
    }

    public void SwitchToPreviousState() {
        stateMachine.SwtichToPreviousState();
        RestoreAgentState(ref agent);
    }

    public StateMachine GetStateMachine() {
        return stateMachine;
    }
} 
