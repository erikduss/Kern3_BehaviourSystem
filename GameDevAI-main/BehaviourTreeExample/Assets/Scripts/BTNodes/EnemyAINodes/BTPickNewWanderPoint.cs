using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTPickNewWanderPoint : BTBaseNode
{
    private Transform[] waypoints;
    private VariableGameObject target;
    private int currentWaypoint = 0;
    private NavMeshAgent curagent;
    private TextMesh stateText;

    public BTPickNewWanderPoint(Transform[] _waypoints, VariableGameObject targ, NavMeshAgent agent, TextMesh text)
    {
        waypoints = _waypoints;
        target = targ;
        curagent = agent;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "PickNewWanderPoint";

        if(currentWaypoint >= waypoints.Length)
        {
            currentWaypoint = 0;
        }

        if(waypoints[currentWaypoint].gameObject != null)
        {
            target.Value = waypoints[currentWaypoint].gameObject;

            currentWaypoint++;

            return TaskStatus.Success;
        }
        return TaskStatus.Failed;
    }
}
