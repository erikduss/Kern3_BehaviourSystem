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
    private Animator animator;
    private VariableFloat moveSpeed;
    private NavMeshAgent agent;

    private GameObject player;

    private TextMesh stateText;

    public BTIdle(Animator anim, VariableFloat speed, NavMeshAgent rogue, TextMesh text)
    {
        animator = anim;
        moveSpeed = speed;
        agent = rogue;
        player = GameObject.FindGameObjectWithTag("Player");
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "Idle";
        if (Vector3.Distance(agent.gameObject.transform.position, player.transform.position) < 5)
        {
            animator.SetFloat("MoveSpeed", 0);
            return TaskStatus.Running;
        }
        else
        {
            return TaskStatus.Success;
        }
        
    }
}
