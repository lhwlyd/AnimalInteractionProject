using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Grabbed : IState
{
    NavMeshAgent agent;

    public Grabbed(NavMeshAgent agent) {
        this.agent = agent;
    }

    public void Enter()
    {
        agent.isStopped = true;
        agent.enabled = false;
        agent.updatePosition = false;
        // agent.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        // Debug.Log(agent.transform.tag);
    }

    public void Execute()
    {
        Debug.Log("I'm being grabbed!");
    }

    public void Exit()
    {
        agent.enabled = true;
        agent.isStopped = false;
        agent.updatePosition = true;
        // agent.gameObject.GetComponent<NavMeshAgent>().enabled = true;

    }
}
