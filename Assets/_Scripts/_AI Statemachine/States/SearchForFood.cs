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

    private Animation anim;
    private const string animationStr = "Arm_cock|Walk_search";

    public SearchForFood(LayerMask searchLayer, BaseAnimal animal, float searchRadius,
        string tagToLookFor, NavMeshAgent agent, float foodConsumingRate) {
        this.searchLayer = searchLayer;
        this.animal = animal;
        this.searchRadius = searchRadius;
        this.tagToLookFor = tagToLookFor;
        this.agent = agent;
        this.foodConsumingRate = foodConsumingRate;
        anim = agent.gameObject.GetComponent<Animation>();
        if (anim != null)
        {
            Debug.Log("set searching to loop");
            anim[animationStr].wrapMode = WrapMode.Loop;
        }
    }

    public void Enter()
    {
        wanderAround = new WanderAround(agent, agent.speed);
        resting = new Resting(animal, agent);
    }

    public void Execute()
    {
        
        var hitObjects = Physics.OverlapSphere(animal.gameObject.transform.position,
            searchRadius, searchLayer);
        if (hitObjects.Length == 0) {
            // Just wandering around if no food given
            if (animal.GetEnergyLevel() < 20f)
            {
                // resting.Execute(); // Can't do this, because the energy level will just float above and below 20f
                animal.GetStateMachine().ChangeState(resting);
            }
            else
            {
                if (wanderAround.ExecuteManually()) {
                    if (anim != null)
                    {
                        Debug.Log("searching for stuff...");
                        anim.Play(animationStr);
                    }
                }
            }
            return;
        }

        if (animal.GetHungerLevel() > 80f) {
            Debug.Log("no longer needs to search for food");
            // Debug.Break();
            // animal.GetStateMachine().SwtichToPreviousState();
            animal.GetStateMachine().ChangeState(wanderAround);
            return;
        }

        var index = -1;

        // Better performance than foreach
        for (int i=0; i<hitObjects.Length; i++) {
            if (hitObjects[i].CompareTag(tagToLookFor)) {
                agent.SetDestination(hitObjects[i].transform.position);
                if(Vector3.Distance(new Vector3(animal.gameObject.transform.position.x, animal.gameObject.transform.position.y, 0f), 
                    new Vector3(hitObjects[i].transform.position.x, hitObjects[i].transform.position.y, 0f)) < 1f){

                    index = i;
                    break;

                    // Debug.Log(animal.GetStateMachine().GetCurrentState());
                }
            }
            
        }

        if(index > -1)
        {
            
            animal.GetStateMachine().ChangeState(new EatingFood(hitObjects[index].gameObject.GetComponent<Food>(),
                    foodConsumingRate, agent, animal));
        }
    }

    public void Exit()
    {
        Debug.Log("DONE SEARCHING");
        // animal.GetStateMachine().ChangeState(wanderAround);
    }
}
