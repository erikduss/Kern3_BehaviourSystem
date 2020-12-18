using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTPickNewWanderPoint : BTBaseNode
{
    private Transform[] waypoints;
    private VariableGameObject target;
    private int currentWaypoint = 0;

    public BTPickNewWanderPoint(Transform[] _waypoints, VariableGameObject targ)
    {
        waypoints = _waypoints;
        target = targ;
    }

    public override TaskStatus Run()
    {
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
