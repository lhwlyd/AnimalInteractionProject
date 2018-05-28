using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchForFun : IState
{
    private LayerMask searchLayer;
    private BaseAnimal animal;
    private float searchRadius;
    private string tagToLookFor;
    private NavMeshAgent agent;
    private WanderAround wanderAround;

    public SearchForFun(LayerMask searchLayer, BaseAnimal animal, float searchRadius,
        string tagToLookFor, NavMeshAgent agent) {

        this.searchLayer = searchLayer;
        this.animal = animal;
        this.searchRadius = searchRadius;
        this.tagToLookFor = tagToLookFor;
        this.agent = agent;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        var hitObjects = Physics.OverlapSphere(animal.gameObject.transform.position,
            searchRadius, searchLayer);
        if (hitObjects.Length == 0)
        {
            // Just wandering around if no food given
            if (animal.GetEnergyLevel() < 20f)
            {
                // resting.Execute(); // Can't do this, because the energy level will just float above and below 20f
            }
            else
            {
                wanderAround.Execute();
            }
            return;
        }
    }

    public void Exit()
    {
    }
}
