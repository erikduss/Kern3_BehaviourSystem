using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    The search cover node should be active when:

    The enemy agent is chasing (or attacking) the player and the friendly agent has not found cover yet.
*/

public class BTSearchCover : BTBaseNode
{
    private NavMeshAgent agent;

    private TextMesh stateText;

    private List<Transform> coverSpots;


    public BTSearchCover(NavMeshAgent rogue, TextMesh text, List<Transform> cover)
    {
        agent = rogue;
        stateText = text;
        coverSpots = cover;
    }

    public override TaskStatus Run()
    {
        if (coverSpots != null)
        {
            if (agent != null)
            {
                stateText.text = "SearchCover";
                agent.destination = FindClosestCover().position;
                agent.stoppingDistance = 0.1f;
                if (agent.remainingDistance < 0.1f)
                {
                    return TaskStatus.Success;
                }
                return TaskStatus.Running;
            }
        }
        return TaskStatus.Failed;
    }

    private Transform FindClosestCover()
    {
        Transform closestCover = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = agent.transform.position;
        foreach (Transform t in coverSpots)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                closestCover = t;
                minDist = dist;
            }
        }
        return closestCover;
    }
}
