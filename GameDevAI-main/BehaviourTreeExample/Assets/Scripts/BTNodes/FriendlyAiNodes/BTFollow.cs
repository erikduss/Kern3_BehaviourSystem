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
    private VariableFloat moveSpeed;
    private NavMeshAgent agent;

    private GameObject player;

    private TextMesh stateText;

    public BTFollow(Animator anim, VariableFloat speed, NavMeshAgent rogue, TextMesh text)
    {
        animator = anim;
        moveSpeed = speed;
        agent = rogue;
        player = GameObject.FindGameObjectWithTag("Player");
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "Follow";
        //Debug.Log(Vector3.Distance(agent.gameObject.transform.position, player.transform.position));
        if (Vector3.Distance(agent.gameObject.transform.position, player.transform.position) >= 5)
        {
            agent.destination = player.transform.position;
            animator.SetFloat("MoveSpeed", 3);
            return TaskStatus.Running;
        }
        else return TaskStatus.Success;

    }
}
