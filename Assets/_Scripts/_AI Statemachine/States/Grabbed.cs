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
        agent.enabled = false;
    }

    public void Execute()
    {
        Debug.Log("I'm being grabbed!");
    }

    public void Exit()
    {
        agent.enabled = true;
    }
}
