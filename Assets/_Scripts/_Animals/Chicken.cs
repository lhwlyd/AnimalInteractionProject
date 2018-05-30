using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(Grabable))]
[RequireComponent(typeof(Interactable))]
public class Chicken : BaseAnimal
{
    float timer;
    float maxTime;

    public override void Drink(float waterPoints)
    {
        throw new System.NotImplementedException();
    }

    public override void Eat(float foodPoints, Collider food)
    {
    }

    public override void Injured(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    private void Start()
    {
        //ResetTime();
        agent = GetComponent<NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");

        stateMachine.ChangeState(new WanderAround(agent, speed, this));
    }

    private void Update()
    {
        stateMachine.ExecuteStateUpdate();
        // if not resting, check to see if should be resting
        if (BusyState != BusyType.Resting) {
            if (energyLevel < 20f)
            {
                //stateMachine.ChangeState(new Resting(this, agent));
                SetBusy(BusyType.Resting);
            }
        }
        // if not busy, check to see what can be done
        if (BusyState == BusyType.NotBusy) {
            
            if ((IsHungry() || IsThirsty()) && !searching)
            {
                searching = true;
                SetBusy(BusyType.SearchingForResource);
                
            } else
            {
                ExitBusy();
                searching = false;
            }
        }

        UpdateBodyConditions();
    }

    private void UpdateBodyConditions() {
        UpdateEnergyLevel(Time.deltaTime * -0.2f);
        UpdateHungerLevel(Time.deltaTime * -0.08f);
        UpdateThirstLevel(Time.deltaTime * -0.06f);
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Food":
                Eat(5f, other);
                break;
        }

    }


}
