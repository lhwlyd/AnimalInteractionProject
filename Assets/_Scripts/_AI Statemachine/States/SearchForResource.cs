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

    public SearchForResource(LayerMask searchLayer1, LayerMask searchLayer2, BaseAnimal animal, float searchRadius,
     NavMeshAgent agent, float foodConsumingRate)
    {
        this.searchLayer1 = searchLayer1;
        this.searchLayer2 = searchLayer2; // use an array
        this.animal = animal;
        this.searchRadius = searchRadius;
        this.agent = agent;
        this.foodConsumingRate = foodConsumingRate;
    }

    public void Enter()
    {
        wanderAround = new WanderAround(agent, agent.speed);
        animal.SetBusy(2);
    }

    public void Execute()
    {
        var hitObjects = Physics.OverlapSphere(this.animal.gameObject.transform.position,
            this.searchRadius, searchLayer);
        if (hitObjects.Length == 0)
        {
            // Just wandering around if no food given
            wanderAround.Execute();
            return;
        }

        if (animal.GetHungerLevel() > 100f)
        {
            animal.GetStateMachine().ChangeState(wanderAround);
        }
        // Better performance than foreach
        for (int i = 0; i < hitObjects.Length; i++)
        {
            if (hitObjects[i].CompareTag(tagToLookFor))
            {
                this.agent.SetDestination(hitObjects[i].transform.position);
                if (Vector3.Distance(new Vector3(animal.gameObject.transform.position.x, animal.gameObject.transform.position.y, 0f),
                    new Vector3(hitObjects[i].transform.position.x, hitObjects[i].transform.position.y, 0f)) < 1f)
                {

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
