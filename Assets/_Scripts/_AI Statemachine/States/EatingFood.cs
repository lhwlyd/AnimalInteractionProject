using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EatingFood : IState
{
    Food food;
    float consumingRate;
    NavMeshAgent agent;
    BaseAnimal animal;
    
    public EatingFood(Food food, float consumingRate, NavMeshAgent agent, BaseAnimal animal) {
        this.food = food;
        this.consumingRate = consumingRate;
        this.agent = agent;
        this.animal = animal;
    }
    public void Enter()
    {
        food.Eaten(consumingRate, animal);
        animal.RecordAgentState(ref agent);
        agent.SetDestination(food.gameObject.transform.position);
    }

    public void Execute()
    {
        food.Consumed(consumingRate);
    }

    public void Exit()
    {
        food.StopEating(animal);
    }
}
