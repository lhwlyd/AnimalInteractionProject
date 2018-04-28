using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Turtle : BaseAnimal
{
    float timer;
    float maxTime;

    public override void Drink(float waterPoints)
    {
        throw new System.NotImplementedException();
    }

    public override void Eat(float foodPoints, Collider food)
    {
        Debug.Log("Start eating food");
        stateMachine.ChangeState(new EatingFood(food.gameObject.GetComponent<Food>(), 
            foodConsumingRate, agent, this));
    }

    public override void Injured(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Move(Vector3 destination)
    {
        //stateManager.SetBaseState(AnimalState.WALKING);
        agent.SetDestination(destination);
    }

    /*
    private void ResetTime() {
        timer = 0;
        maxTime = Random.Range(2f, 5.0f);
    }
    */

    private void Start()
    {
        //ResetTime();
        agent = GetComponent<NavMeshAgent>();
        playerRef = GameObject.FindGameObjectWithTag("Player");

        //stateManager = new AnimalState();
        //stateManager.hungerPoints = 60;
        stateMachine.ChangeState(new WanderAround(agent, speed));
    }

    private void Update()
    {
        this.stateMachine.ExecuteStateUpdate();
        if (hungerLevel < 30f)
        {
            this.stateMachine.ChangeState(
                new SearchForFood(foodItemsLayer, this, speed*15f, "Food", agent));
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            // Grab script moved to "Grabbale.cs"
            case "Food":
                Eat(5f, other);
                break;
        }

    }
    private void OnTriggerExit(Collider other) {

        switch (other.tag) {
            case "Food":
                Debug.Log("left food!");
                //other.gameObject.SendMessage("StopEating", this);
                break;
        }
        
    }


}
