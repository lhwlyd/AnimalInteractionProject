using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class Grabbed : IState
{
    NavMeshAgent agent;
    private Animation anim;
    private const string animationStr = "Arm_cock|idle_1";

    public Grabbed(NavMeshAgent agent) {
        this.agent = agent;
        anim = agent.gameObject.GetComponent<Animation>();
        if (anim != null)
        {
            anim[animationStr].wrapMode = WrapMode.Loop;
        }
    }

    public void Enter()
    {
        agent.enabled = false;
    }

    public void Execute()
    {
        Debug.Log("I'm being grabbed!");
        if (anim != null)
        {
            anim.Play(animationStr);
        }
    }

    public void Exit()
    {
        agent.enabled = true;

        if (anim != null)
        {
            anim.Stop();
        }
    }
}
