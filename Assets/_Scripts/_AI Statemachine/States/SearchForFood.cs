using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SearchForFood : IState
{
    private LayerMask searchLayer;
    private BaseAnimal owner;
    private float searchRadius;
    private string tagToLookFor;
    private NavMeshAgent agent;


    public SearchForFood(LayerMask searchLayer, BaseAnimal owner, float searchRadius,
        string tagToLookFor, NavMeshAgent agent) {
        this.searchLayer = searchLayer;
        this.owner = owner;
        this.searchRadius = searchRadius;
        this.tagToLookFor = tagToLookFor;
        this.agent = agent;
    }

    public void Enter()
    {
    }

    public void Execute()
    {
        var hitObjects = Physics.OverlapSphere(this.owner.gameObject.transform.position,
            this.searchRadius);
        // Better performance than foreach
        for (int i=0; i<hitObjects.Length; i++) {
            if (hitObjects[i].CompareTag(tagToLookFor)) {
                this.agent.SetDestination(hitObjects[i].transform.position);
            }
            break;
        }
    }

    public void Exit()
    {
        throw new System.NotImplementedException();
    }
}
