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
        Debug.Log("Stop eating food");
        object[] tempStorage = new object[2];
        tempStorage[0] = foodPoints; // the consumption rate
        tempStorage[1] = this;
        food.gameObject.SendMessage("StopEating", tempStorage);
    }

    public override void Injured(float damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Move(Vector3 destination)
    {
        agent.SetDestination(destination);
    }

    public void wanderAround()
    {
        Vector3 point;
        float range = 5.0f;

        if (RandomPoint(transform.position, range, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
        }

        Move(point);
    }

    //https://docs.unity3d.com/540/Documentation/ScriptReference/NavMesh.SamplePosition.html
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
            for (int i = 0; i < 30; i++)
            {
                Vector3 randomPoint = center + Random.insideUnitSphere * range;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
                {
                    result = hit.position;
                    return true;
                }
            }
        result = Vector3.zero;
        return false;
    }

    private void ResetTime() {
        timer = 0;
        maxTime = Random.Range(2f, 5.0f);
    }

    private void Start()
    {
        ResetTime();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = 2.0f;
        playerRef = GameObject.FindGameObjectWithTag("Player");

        stateManager = new AnimalState();
    }

    private void Update()
    {
        if (timer >= maxTime)
        {
            ResetTime();
            switch (stateManager.GetMovingState()) {
                case 0: // AnimalState.WANDERING
                    wanderAround();
                    break;

                default:
                    break;
            }
        }
        else {
            timer += Time.deltaTime;
        }

    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag)
        {
            // Grab script moved to "Grabbale.cs"
            case "Food":
                if (stateManager.GetHungerState() > AnimalState.TOO_FULL)
                {
                    Eat(5f, other);
                }
                break;
        }

    }
    private void OnTriggerExit(Collider other) {

        switch (other.tag) {
            case "Food":
                other.gameObject.SendMessage("StopEating", this);
                break;
        }
        
    }


}
