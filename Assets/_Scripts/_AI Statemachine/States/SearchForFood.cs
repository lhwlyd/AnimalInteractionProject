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


    public SearchForFood(LayerMask searchLayer, BaseAnimal animal, float searchRadius,
        string tagToLookFor, NavMeshAgent agent) {
        this.searchLayer = searchLayer;
        this.animal = animal;
        this.searchRadius = searchRadius;
        this.tagToLookFor = tagToLookFor;
        this.agent = agent;

        wanderAround = new WanderAround(agent, agent.speed);
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        var hitObjects = Physics.OverlapSphere(this.animal.gameObject.transform.position,
            this.searchRadius, searchLayer);
        if (hitObjects.Length == 0) {
            // Just wandering around if no food given
            wanderAround.Execute();
            return;
        }

        if (animal.GetHungerLevel() > 100f) {
            animal.GetStateMachine().ChangeState(wanderAround);
        }
        // Better performance than foreach
        for (int i=0; i<hitObjects.Length; i++) {
            if (hitObjects[i].CompareTag(tagToLookFor)) {
                this.agent.SetDestination(hitObjects[i].transform.position);
            }
            return;
        }

        // No food found, stay where it is
    }

    public void Exit()
    {
    }
}
