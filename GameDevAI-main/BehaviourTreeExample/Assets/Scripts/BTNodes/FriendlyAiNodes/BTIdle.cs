using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    The idle node should be active when:

    The distance between the friendly agent and the player is not larger than 5 units. And the player is not spotted by the enemy agent. (or if the player escaped)
*/

public class BTIdle : BTBaseNode
{
    private NavMeshAgent agent;

    private GameObject objectToFollow;

    private TextMesh stateText;

    public BTIdle(NavMeshAgent rogue, GameObject _objectToFollow, TextMesh text)
    {
        agent = rogue;
        objectToFollow = _objectToFollow;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "Idle";

        if (Vector3.Distance(agent.gameObject.transform.position, objectToFollow.transform.position) < 5)
        {
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
    }
}
