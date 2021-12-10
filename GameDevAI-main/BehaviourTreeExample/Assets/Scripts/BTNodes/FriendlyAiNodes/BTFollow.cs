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
    private Animator animator;
    private NavMeshAgent agent;

    private GameObject player;
    private Player playerScript;

    private TextMesh stateText;

    public BTFollow(Animator anim, NavMeshAgent rogue, TextMesh text, Player script)
    {
        animator = anim;
        agent = rogue;
        player = GameObject.FindGameObjectWithTag("Player");
        stateText = text;
        playerScript = script;
    }

    public override TaskStatus Run()
    {
        if (!playerScript.isBeingChased)
        {
            stateText.text = "Follow";
            //Debug.Log(Vector3.Distance(agent.gameObject.transform.position, player.transform.position));
            if (Vector3.Distance(agent.gameObject.transform.position, player.transform.position) >= 5)
            {
                agent.stoppingDistance = 5f;
                agent.destination = player.transform.position;
                animator.SetFloat("MoveSpeed", 3);
                return TaskStatus.Running;
            }
            else return TaskStatus.Success;
        }
        return TaskStatus.Success;
    }
}
