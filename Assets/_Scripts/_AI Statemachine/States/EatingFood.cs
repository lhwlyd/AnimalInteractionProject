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

    private Animation anim;
    private const string animationStr = "Arm_cock|eat";

    public EatingFood(Food food, float consumingRate, NavMeshAgent agent, BaseAnimal animal) {
        this.food = food;
        this.consumingRate = consumingRate;
        this.agent = agent;
        this.animal = animal;

        if (anim != null)
        {
            anim[animationStr].wrapMode = WrapMode.Loop;
        }
    }

    public void Enter()
    {
        food.Eaten(consumingRate, animal);
        animal.RecordAgentState(ref agent);
        // agent.SetDestination(food.gameObject.transform.position);
        // Debug.Log(animal.name + " start eating");
        animal.SetBusy(BaseAnimal.BusyType.Ingesting);
    }

    public void Execute()
    {
	    food.Consumed(animal);

        if (anim != null)
        {
            anim.Play(animationStr);
        }
    }

    public void Exit()
    {
        animal.ExitBusy();
        food.StopEating(animal);
        // animal.RestoreAgentState(ref agent);
    }
}
