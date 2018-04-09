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

    public override void Drink(float waterPoints)
    {
        throw new System.NotImplementedException();
    }

    public override void Eat(float foodPoints)
    {
        throw new System.NotImplementedException();
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
        animalGesturesRef = playerRef.GetComponent<AnimalGestures>();
    }

    private void Update()
    {
        if (timer >= maxTime)
        {
            ResetTime();
            wanderAround();
        }
        else {
            timer += Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("leftHand")) {
            Debug.Log("Grabbing with left hand");
            if (animalGesturesRef.LeftHandGrabStrength() > 0.5f)
            {
                transform.parent = other.transform;
                GetComponent<NavMeshAgent>().enabled = false;
            }
            else {
                transform.parent = null;
                GetComponent<NavMeshAgent>().enabled = true;
            }
        }
        else if(other.CompareTag("rightHand")) {
            Debug.Log("Grabbing with right hand");
            if (animalGesturesRef.RightHandGrabStrength() > 0.5f)
            {
                transform.parent = other.transform;
                GetComponent<NavMeshAgent>().enabled = false;
            }
            else {
                transform.parent = null;
                GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        transform.parent = null;
        GetComponent<NavMeshAgent>().enabled = true;
    }

}
