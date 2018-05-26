using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WanderAround : IState
{
    private NavMeshAgent agent;
    private float speed;
    private Animation anim;
    private const string animationStr = "Arm_cock|Walk_fast";

    public WanderAround(NavMeshAgent agent, float speed) {
        this.agent = agent;
        this.speed = speed;
        anim = agent.gameObject.GetComponent<Animation>();
        if (anim != null) {
            anim[animationStr].wrapMode = WrapMode.Loop;
        }
    }
    public void Enter()
    {
        agent.GetComponent<BaseAnimal>().ExitBusy();
        if (MoveToNewPlace()) {
            if (anim != null)
            {
                anim.Play(animationStr);
            }
        }
    }

    public void Execute()
    {

        Debug.Log("Wandering around");
        if (agent.isActiveAndEnabled)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        if (MoveToNewPlace())
                        {
                            if (anim != null)
                            {
                                anim.Play(animationStr);
                            }
                        }
                    }
                }
            }
        }
    }

    public void Exit()
    {
        if (anim != null)
        {
            anim.Stop();
        }
        if (agent.isActiveAndEnabled)
            agent.SetDestination(agent.gameObject.transform.position);
    }

    //Reference: https://docs.unity3d.com/540/Documentation/ScriptReference/NavMesh.SamplePosition.html
    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                Debug.DrawLine(Vector3.zero, result);
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

    public bool ExecuteManually() {
        if (agent.isActiveAndEnabled)
        {
            if (!agent.pathPending)
            {
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
                    {
                        return MoveToNewPlace();
                    }
                }
            }
        }
        return false;
    }

    private bool MoveToNewPlace() {
        Vector3 point;
        if (RandomPoint(agent.gameObject.transform.position, speed * 5f, out point))
        {
            Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
        }

        if (agent.isActiveAndEnabled)
        {
            agent.SetDestination(point);
            agent.speed = speed;
            return true;
        }
        return false;
    }
}
