using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FetchState : IState {

    NavMeshAgent agent;
    private Animation anim;
    private const string animationStr = "Arm_cock|Walk_fast";

    public FetchState(NavMeshAgent agent)
    {
        this.agent = agent;
        anim = agent.gameObject.GetComponent<Animation>();
        if (anim != null)
        {
            anim[animationStr].wrapMode = WrapMode.Loop;
        }
    }

    public void Enter()
    {
        
    }

    public void Execute()
    {
        Debug.Log("Fetching");
        if(anim != null)
        {
            anim.Play(animationStr);
        }
    }

    public void Exit()
    {
        if(anim != null)
        {
            anim.Stop();
        }
    }
}
