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

        stateMachine.ChangeState(new WanderAround(agent, speed));
    }

    private void Update()
    {
        stateMachine.ExecuteStateUpdate();
        if (!IsBusy()) {
            if ((hungerLevel < 30f || thirstLevel < 30f))
            {
                stateMachine.ChangeState(
                    new SearchForResource(foodItemsLayer, waterItemsLayer, this, speed * 15f, agent, foodConsumingRate, waterConsumingRate));
            }

            if (energyLevel < 20f) {
                stateMachine.ChangeState(new Resting(this, agent));
            }
        }

        UpdateBodyConditions();
    }

    private void UpdateBodyConditions() {
        UpdateEnergyLevel(Time.deltaTime * -0.2f);
        UpdateHungerLevel(Time.deltaTime * -0.8f);
        UpdateThirstLevel(Time.deltaTime * -0.6f);
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
