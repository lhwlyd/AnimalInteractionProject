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
        Debug.Log(stateMachine.GetCurrentState());
        this.stateMachine.ExecuteStateUpdate();
        if (!this.IsBusy()) {
            if ((hungerLevel < 30f || thirstLevel < 30f))
            {
                Debug.Log("change state");
                this.stateMachine.ChangeState(
                    new SearchForResource(foodItemsLayer, waterItemsLayer, this, speed * 15f, agent, foodConsumingRate, waterConsumingRate));
            }

            if (this.energyLevel < 20f) {
                Debug.Log("Get to sleep");
                this.stateMachine.ChangeState(new Resting(this, this.agent));
            }
        }


        /*
        if (hungerLevel > 100f && this.stateMachine.GetCurrentState().GetType().Equals("EatingFood")) {
            Debug.Log("Swtich");
            this.SwitchToPreviousState();
        }
        */

        this.UpdateBodyConditions();
    }

    private void UpdateBodyConditions() {
        this.UpdateEnergyLevel(Time.deltaTime * -0.2f);
        this.UpdateHungerLevel(Time.deltaTime * -0.1f);
        this.UpdateThirstLevel(Time.deltaTime * -0.05f);
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
                //Debug.Log("left food!");
                //other.gameObject.SendMessage("StopEating", this);
                break;
        }
        
    }


}
