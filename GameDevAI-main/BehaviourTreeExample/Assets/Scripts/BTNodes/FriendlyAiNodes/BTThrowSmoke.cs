using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    The thow smoke node should be active when:

    The friendly agent found cover and the player is still getting chases/attacked.
*/

public class BTThrowSmoke : BTBaseNode
{
    private NavMeshAgent agent;
    private Player playerScript;
    private TextMesh stateText;

    private Animator anim;
    private Rogue rogueScript;

    private VariableFloat canThrowBomb;

    public BTThrowSmoke(NavMeshAgent rogue, TextMesh text, Player script, Animator animator, Rogue parentScript, VariableFloat canThrow)
    {
        agent = rogue;
        stateText = text;
        playerScript = script;
        anim = animator;
        rogueScript = parentScript;
        canThrowBomb = canThrow;
    }

    public override TaskStatus Run()
    {
        if (playerScript.isBeingChased)
        {
            stateText.text = "ThrowSmoke";

            Debug.Log(canThrowBomb.Value);

            if (canThrowBomb.Value > 0)
            {
                Debug.Log("Thowing");
                anim.Play("Throw");
                rogueScript.SpawnSmokeBomb();
            }

            return TaskStatus.Success;
        }
        return TaskStatus.Failed;
    }
}
