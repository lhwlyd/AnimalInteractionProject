using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public abstract class BaseAnimal: MonoBehaviour {

    [SerializeField]
    protected GameObject playerRef;
    [SerializeField]
    protected LayerMask foodItemsLayer;
    [SerializeField]
    protected float foodDetectRange;

    [SerializeField]
    [Range(0, 100)] protected float agressionLevel,
        hungerLevel,
        intimateLevel,
        intelligenceLevel,
        thirstLevel,
        healthLevel,
        foodConsumingRate,
        waterConsumingRate,
        speed;
    protected bool isBusy;
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
        this.hungerLevel += update;
    }

    public float GetThirstLevel()
    {
        return thirstLevel;
    }

    public void UpdateThirstLevel(float update)
    {
        this.thirstLevel += update;
    }

    public void SetBusy(int busyType) {
        isBusy = true;
        switch (busyType) {
            // Being grabbed
            case 0:
                RecordAgentState(ref agent);
                this.stateMachine.ChangeState(new Grabbed(agent));
            break;
                
        }

    }

    public void ExitBusy(){
        isBusy = false;
    }

    public void SwitchToPreviousState() {
        stateMachine.SwtichToPreviousState();
        RestoreAgentState(ref agent);
    }

    public StateMachine GetStateMachine() {
        return this.stateMachine;
    }
} 
