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

    public SearchForFun(LayerMask searchLayer, BaseAnimal animal, float searchRadius, // don't need the radius now
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
        // Find the laser dot using the tag.
    }

    public void Exit()
    {
    }
}
