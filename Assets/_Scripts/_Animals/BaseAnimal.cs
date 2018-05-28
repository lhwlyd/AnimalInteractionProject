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
    public bool isBusy;
    public BusyType BusyState = BusyType.NotBusy;
    public enum BusyType
    {
        NotBusy,
        Grabbed,
        Hurt,
        Ingesting,
        SearchingForResource,
        Resting,
        Fetching
    };

    protected NavMeshAgent agent;

    // Nav mesh vars
    protected Vector3 lastAgentVelocity;
    protected NavMeshPath lastAgentPath;
    protected Vector3 lastAgentDestination;
    
    public bool searching;

    public float hungerLimit = 30.0f;
    public float thirstLimit = 30.0f;
    //protected AnimalState stateManager;
    protected StateMachine stateMachine = new StateMachine();

    public abstract void Move(Vector3 destination);

    public abstract void Eat(float foodPoints, Collider food);

    public abstract void Drink(float waterPoints);

    public abstract void Injured(float damage);

    public bool IsHungry() {
        return hungerLevel < hungerLimit;
    }

    public bool IsThirsty() {
        return thirstLevel < thirstLimit;
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

    public float GetSpeed()
    {
        return speed;
    }

    public void UpdateAggresionLevel(float update) {
        aggressionLevel += update;
    }

    public float GetAggression() {
        return aggressionLevel;
    }

    public NavMeshAgent GetAgent() {
        return agent;
    }

    public void SetBusy(BusyType busyType) {
        if (busyType == BusyType.NotBusy) return;
        isBusy = true;
        BusyState = busyType;
        switch (busyType) {
            case BusyType.Grabbed:
                RecordAgentState(ref agent);
                stateMachine = new StateMachine();
                stateMachine.ChangeState(new Grabbed(agent));
            break;

            case BusyType.SearchingForResource:
                stateMachine.ChangeState(
                    new SearchForResource(foodItemsLayer, waterItemsLayer, this, speed * 15f, agent, foodConsumingRate, waterConsumingRate));
                break;

            case BusyType.Hurt:
                break;

            case BusyType.Ingesting:
                break;

            case BusyType.Resting:
                stateMachine.ChangeState(new Resting(this, agent));
                break;

            case BusyType.Fetching:
                RecordAgentState(ref agent);
                stateMachine.ChangeState(new FetchState(agent));
                break;
        }

    }

    public void ResetStateMachine()
    {
        stateMachine = new StateMachine();
    }

    public void ExitBusy(){
        isBusy = false;
        BusyState = BusyType.NotBusy;
    }

    public void ExitBusyWithStateChange(IState state)
    {
        isBusy = false;
        stateMachine.ChangeState(state);
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

    void OnLaser(Vector3 point)
    {
        if (BusyState != BusyType.NotBusy || IsThirsty() || IsHungry())  return;
        if (!isBusy)
        {
            Debug.Log("Activate laser chasing state here");
            Debug.Log(point);
            agent.ResetPath();
            agent.SetDestination(point);
        }
    }

    void OnFetch(Throwable other) {
        Debug.Log("Fetching object at: " + other.transform.position);
        // if (IsThirsty() || IsHungry()) return;
        if (BusyState == BusyType.SearchingForResource) {
            return;
        }
        else {
            SetBusy(BusyType.Fetching);
        }
        var pos = other.transform.position;

        agent.SetDestination(pos);
        var distance = Vector3.Distance(pos, transform.position);
        Debug.Log(distance);
        if (distance < 1.0f)
        {

            var fetch = transform.Find("FetchLoc");
            if (fetch == null)
                other.transform.parent = transform;
            else
            {
                var scale = other.transform.localScale;
                other.transform.position = Vector3.zero;
                other.transform.parent = fetch;
                other.transform.localRotation = Quaternion.identity;
                other.transform.localPosition = Vector3.zero;
                
            }
            other.Reset();
        }
    }

    void OnReturnFetch(Throwable other)
    {
        Debug.Log("Returning object to player");

        var pos = playerRef.transform.position;
        agent.SetDestination(pos);

        var distance = Vector3.Distance(transform.position, pos);
        Debug.Log(distance);
        if (distance < 1.5f)
        {
            other.ReturnThrowable();
            other.transform.parent = null;
            
            ExitBusyWithStateChange(new WanderAround(agent, speed));
        }
    }
} 
