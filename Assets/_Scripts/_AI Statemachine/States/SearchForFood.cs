using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchForFood : IState
{
    private LayerMask searchLayer;
    private BaseAnimal animal;
    private float searchRadius;
    private string tagToLookFor;
    private NavMeshAgent agent;
    private WanderAround wanderAround;
    private Resting resting;
    private float foodConsumingRate;

    public SearchForFood(LayerMask searchLayer, BaseAnimal animal, float searchRadius,
        string tagToLookFor, NavMeshAgent agent, float foodConsumingRate) {
        this.searchLayer = searchLayer;
        this.animal = animal;
        this.searchRadius = searchRadius;
        this.tagToLookFor = tagToLookFor;
        this.agent = agent;
        this.foodConsumingRate = foodConsumingRate;
    }

    public void Enter()
    {
        wanderAround = new WanderAround(agent, agent.speed);
        resting = new Resting(animal, agent);
    }

    public void Execute()
    {
        var hitObjects = Physics.OverlapSphere(this.animal.gameObject.transform.position,
            this.searchRadius, searchLayer);
        if (hitObjects.Length == 0) {
            // Just wandering around if no food given
            if (animal.GetEnergyLevel() < 20f)
            {
                // resting.Execute(); // Can't do this, because the energy level will just float above and below 20f
                this.animal.GetStateMachine().ChangeState(resting);
            }
            else {
                wanderAround.Execute();
            }
            return;
        }

        if (animal.GetHungerLevel() > 80f && animal.GetThirstLevel() > 50f) {
            animal.GetStateMachine().ChangeState(wanderAround);
        }
        // Better performance than foreach
        for (int i=0; i<hitObjects.Length; i++) {
            if (hitObjects[i].CompareTag(tagToLookFor)) {
                this.agent.SetDestination(hitObjects[i].transform.position);
                if(Vector3.Distance(new Vector3(animal.gameObject.transform.position.x, animal.gameObject.transform.position.y, 0f), 
                    new Vector3(hitObjects[i].transform.position.x, hitObjects[i].transform.position.y, 0f)) < 1f){
                        
                    animal.GetStateMachine().ChangeState(new EatingFood(hitObjects[i].gameObject.GetComponent<Food>(), 
                    foodConsumingRate, agent, animal));


                    Debug.Log(animal.GetStateMachine().GetCurrentState());
                }
            }
            return;
        }

        // No food found, stay where it is
    }

    public void Exit()
    {
    }
}
