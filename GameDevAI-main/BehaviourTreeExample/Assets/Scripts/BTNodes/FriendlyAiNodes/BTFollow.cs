using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    The follow node should be active when:

    The distance between the friendly agent and the player is bigger than 5 units and the player is not in combat.
*/

public class BTFollow : BTBaseNode
{
    private NavMeshAgent agent;

    private GameObject objectToFollow;

    private TextMesh stateText;

    public BTFollow(NavMeshAgent rogue, GameObject _objectToFollow, TextMesh text)
    {
        agent = rogue;
        objectToFollow = _objectToFollow;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "Follow";

        if (Vector3.Distance(agent.gameObject.transform.position, objectToFollow.transform.position) >= 5)
        {
            agent.stoppingDistance = 5f;
            agent.destination = objectToFollow.transform.position;
            return TaskStatus.Running;
        }
        else return TaskStatus.Success;
    }
}
