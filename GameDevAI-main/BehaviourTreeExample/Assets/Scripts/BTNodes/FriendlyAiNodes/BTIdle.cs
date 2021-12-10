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
    private NavMeshAgent agent;

    private GameObject player;
    private Player playerScript;

    private TextMesh stateText;

    public BTIdle(Animator anim, NavMeshAgent rogue, TextMesh text, Player script)
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
        return TaskStatus.Success;
    }
}
