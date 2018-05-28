using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Resting : IState
{
    private BaseAnimal animal;
    private NavMeshAgent agent;
    private Animation anim;
    private const string animationStr = "Arm_cock|idle_1";

    public Resting(BaseAnimal animal, NavMeshAgent agent) {
        this.animal = animal;
        this.agent = agent;
        anim = agent.gameObject.GetComponent<Animation>();
        if (anim != null)
        {
            anim[animationStr].wrapMode = WrapMode.Loop;
        }
    }

    public void Enter()
    {
        // animal.SetBusy(BaseAnimal.BusyType.Resting);

        //if(agent.isActiveAndEnabled)
        //    agent.SetDestination(animal.gameObject.transform.position);

        // Play sound(snoring), animations
        agent.isStopped = true;
    }

    public void Execute()
    {
        animal.UpdateEnergyLevel(Time.deltaTime * 2f);
        if (anim != null)
        {
            anim.Play(animationStr);
        }

        if (animal.GetEnergyLevel() >= 80f)
        {
            // No need to rest
            animal.ExitBusy();
            //animal.GetStateMachine().SwtichToPreviousState();
            animal.GetStateMachine().ChangeState(new WanderAround(agent, animal.GetSpeed()));
        }
    }

    public void Exit()
    {
        if (anim != null)
        {
            anim.Stop();
        }
        agent.isStopped = false;
        // animal.ExitBusy();
    }
}
