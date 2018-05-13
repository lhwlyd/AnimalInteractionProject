using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Resting : IState
{
    private BaseAnimal animal;
    private NavMeshAgent agent;

    public Resting(BaseAnimal animal, NavMeshAgent agent) {
        this.animal = animal;
        this.agent = agent;
    }

    public void Enter()
    {
        animal.SetBusy(BaseAnimal.BusyType.Resting);
        agent.SetDestination(animal.gameObject.transform.position);

        // Play sound(snoring), animations
    }

    public void Execute()
    {
        animal.UpdateEnergyLevel(Time.deltaTime * 2f);

        if (animal.GetEnergyLevel() >= 80f) {
            // No need to rest
            animal.GetStateMachine().SwtichToPreviousState();
        }
    }

    public void Exit()
    {

        // Play transition animations
    }
}
