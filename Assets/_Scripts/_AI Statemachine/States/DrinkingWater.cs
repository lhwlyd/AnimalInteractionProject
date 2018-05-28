using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DrinkingWater : IState
{
    Water water;
    float consumingRate;
    NavMeshAgent agent;
    BaseAnimal animal;

    public DrinkingWater(Water water, float consumingRate, NavMeshAgent agent, BaseAnimal animal)
    {
        this.water = water;
        this.consumingRate = consumingRate;
        this.agent = agent;
        this.animal = animal;
    }

    public void Enter()
    {
        water.Eaten(consumingRate, animal);
        animal.RecordAgentState(ref agent);
        agent.SetDestination(water.gameObject.transform.position);
        // Debug.Log(animal.name + " start eating");
        animal.SetBusy(BaseAnimal.BusyType.Ingesting);
    }

    public void Execute()
    {
        water.Consumed(animal);
    }

    public void Exit()
    {
        animal.ExitBusy();
        water.StopEating(animal);
        animal.RestoreAgentState(ref agent);
        // Debug.Log(animal.name + " stopped eating");
    }
}
