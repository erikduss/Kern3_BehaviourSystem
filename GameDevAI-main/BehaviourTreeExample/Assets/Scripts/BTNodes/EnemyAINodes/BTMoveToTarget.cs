using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTMoveToTarget : BTBaseNode
{
    private VariableGameObject destination;
    private float speed;
    private NavMeshAgent agent;
    private float stoppingDistance;
    private TextMesh stateText;

    public BTMoveToTarget(NavMeshAgent _agent, float movespeed, VariableGameObject _destination, float stoppingDist, TextMesh text)
    {
        agent = _agent;
        destination = _destination;
        speed = movespeed;
        stoppingDistance = stoppingDist;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "MoveToTarget";

        if(agent != null && destination != null)
        {
            agent.speed = speed;
            agent.stoppingDistance = stoppingDistance;
            agent.destination = destination.Value.transform.position;

            if(Vector3.Distance(agent.transform.position, destination.Value.transform.position) < (stoppingDistance + 0.1f))
            {
               return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }
        return TaskStatus.Failed;
    }
}
