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
    
    private Player playerScript;

    private TextMesh stateText;

    private List<Transform> coverSpots;

    private Animator anim;


    public BTSearchCover(Animator animator, NavMeshAgent rogue, TextMesh text, Player script, List<Transform> cover)
    {
        agent = rogue;
        stateText = text;
        playerScript = script;
        coverSpots = cover;
        anim = animator;
    }

    public override TaskStatus Run()
    {
        if (playerScript.isBeingChased)
        {
            if (coverSpots != null)
            {
                if (agent != null)
                {
                    stateText.text = "SearchCover";
                    anim.SetFloat("MoveSpeed", 3);
                    agent.destination = FindClosestCover().position;
                    agent.stoppingDistance = 0.1f;
                    if (agent.remainingDistance < 0.1f)
                    {
                        anim.SetFloat("MoveSpeed", 0);
                        return TaskStatus.Success;
                    }
                    return TaskStatus.Running;
                }
            }
            return TaskStatus.Failed;
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
