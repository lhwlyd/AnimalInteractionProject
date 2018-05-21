using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Grabbed : IState
{
    NavMeshAgent agent;
    GameObject GoRef;

    public Grabbed(NavMeshAgent agent) {
        this.agent = agent;
        GoRef = agent.gameObject;
        agent.enabled = false;
    }

    public void Enter()
    {
        // GameObject.Destroy(GoRef.GetComponent<NavMeshAgent>(), 0);
    }

    public void Execute()
    {
        Debug.Log("I'm being grabbed!");
    }

    public void Exit()
    {
        // Debug.Break();
        GoRef.GetComponent<NavMeshAgent>().enabled = true;
    }
}
