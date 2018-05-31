using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchForResource : IState
{
    private LayerMask searchLayer1;
    private LayerMask searchLayer2;
    private BaseAnimal animal;
    private float searchRadius;
    private string tagToLookFor;
    private NavMeshAgent agent;
    private WanderAround wanderAround;
    private float foodConsumingRate;
    private float waterConsumingRate;

    private SearchForFood searchForFood;
    private SearchForWater searchForWater;
    //private StateMachine microManager;


    public SearchForResource(LayerMask searchLayer1, LayerMask searchLayer2, BaseAnimal animal, float searchRadius,
     NavMeshAgent agent, float foodConsumingRate, float waterConsumingRate)
    {
        this.searchLayer1 = searchLayer1;
        this.searchLayer2 = searchLayer2; // use an array
        this.animal = animal;
        this.searchRadius = searchRadius;
        this.agent = agent;
        this.foodConsumingRate = foodConsumingRate;
        this.waterConsumingRate = waterConsumingRate;

    }

    public void Enter()
    {
        wanderAround = new WanderAround(agent, agent.speed, animal);

        searchForFood = new SearchForFood(searchLayer1, animal, searchRadius, "Food", agent, foodConsumingRate);
        searchForWater = new SearchForWater(searchLayer2, animal, searchRadius, "Water", agent, waterConsumingRate);

        //animal.SetBusy(BaseAnimal.BusyType.SearchingForResource);
    }

    public void Execute()
    {
        //Debug.Log("looking for resources");

        if (animal.GetHungerLevel() < animal.GetThirstLevel())
        // So far this condition checking should be safe.
        {
            // Debug.Log("Searching for food!");
            animal.GetStateMachine().ChangeState(searchForFood);
        }
        else if (animal.GetHungerLevel() > animal.GetThirstLevel())
        {
            // Debug.Log("Searching for water!");
            animal.GetStateMachine().ChangeState(searchForWater);
        }
        else
        {
            animal.GetStateMachine().ChangeState(new WanderAround(agent, animal.GetSpeed(), animal));
        }
    }

    public void Exit()
    {
        // animal.ExitBusy();
    }
}
