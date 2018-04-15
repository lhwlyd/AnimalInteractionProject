using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Turtle : BaseAnimal
{
    float timer;
    float maxTime;
    AnimalGestures animalGesturesRef;

    bool readyToBeGrabbed;
    Collider grabbingHand;
    bool eating; // Can create a status class later if it gets complicated

    public override void Drink(float waterPoints)
    {
        throw new System.NotImplementedException();
    }

    public override void Eat(float foodPoints, Collider food)
    {
        eating = true;
        object[] tempStorage = new object[2];
        tempStorage[0] = foodPoints; // the consumption rate
        tempStorage[1] = this;
        food.gameObject.SendMessage("Eaten", tempStorage);
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
        agent.speed = 5.0f;
        playerRef = GameObject.FindGameObjectWithTag("Player");
        animalGesturesRef = playerRef.GetComponent<AnimalGestures>();

        hungerLevel = 100;
    }

    private void Update()
    {
        hungerLevel -= 0.1f * Time.deltaTime;

        if (hungerLevel <= 100f)
        {
            LookForFood();
        }
        else {
            if (timer >= maxTime)
            {
                ResetTime();
                wanderAround();
            }
            else
            {
                timer += Time.deltaTime;
            }
        }

        // Grabbed?
        if (readyToBeGrabbed) {
            if (animalGesturesRef.LeftHandGrabStrength() > 0.5f)
            {
                Grabbed();
            }
            else {
                ReleaseGrabbing();
            }
        }

        
    }

    private void Grabbed() {
        transform.parent = grabbingHand.transform;
        lastAgentVelocity = agent.velocity;
        lastAgentPath = agent.path;
        lastAgentDestination = agent.destination;

        agent.velocity = Vector3.zero;
        agent.ResetPath();
    }

    private void ReleaseGrabbing()
    {
        grabbingHand = null;
        transform.parent = null;

        //GetComponent<NavMeshAgent>().enabled = true;
        readyToBeGrabbed = false;

        agent.velocity = lastAgentVelocity;
        wanderAround();
    }

    private void OnTriggerEnter(Collider other)
    {

        switch (other.tag) {
            case "leftHand":
                grabbingHand = other;
                Debug.Log("Grabbing with left hand");
                if (animalGesturesRef.LeftHandGrabStrength() < 0.3f)
                {
                    readyToBeGrabbed = true;
                }
                else
                {

                }
                break;

            case "rightHand":
                grabbingHand = other;
             
                Debug.Log("Grabbing with right hand");
                if (animalGesturesRef.RightHandGrabStrength() < 0.3f)
                {
                    readyToBeGrabbed = true;
                }
                else
                {
                }
                
                break;

            case "Food":
                Debug.Log("Begin eating food");
                Eat(5f, other);
                break;
    }
        
    }

    private void OnTriggerExit(Collider other) {

        switch (other.tag) {
            case "leftHand":
            case "rightHand":
                ReleaseGrabbing();
                break;

            case "Food":
                Debug.Log("Stop eating food");
                object[] tempStorage = new object[2];
                tempStorage[0] = 5f; // the consumption rate
                tempStorage[1] = this;
                other.gameObject.SendMessage("StopEating", tempStorage);
                break;
        }
        
    }


}
