using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveToTarget : BTBaseNode
{
    private VariableGameObject target;
    private float speed;
    private NavMeshAgent agent;
    private float stoppingDistance;
    private TextMesh stateText;

    public BTMoveToTarget(NavMeshAgent _agent, float movespeed, VariableGameObject _target, float stoppingDist, TextMesh text)
    {
        agent = _agent;
        target = _target;
        speed = movespeed;
        stoppingDistance = stoppingDist;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "MoveToTarget";

        if(agent != null && target != null)
        {
            agent.speed = speed;
            agent.stoppingDistance = stoppingDistance;
            agent.destination = target.Value.transform.position;
            if (agent.remainingDistance < 0.1f)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }
        return TaskStatus.Failed;
    }
}
