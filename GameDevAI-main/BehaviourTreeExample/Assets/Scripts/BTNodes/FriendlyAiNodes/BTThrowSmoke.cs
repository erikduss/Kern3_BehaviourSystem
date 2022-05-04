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
    private TextMesh stateText;

    private Animator anim;
    private Rogue rogueScript;

    public BTThrowSmoke(Animator animator, Rogue parentScript, TextMesh text)
    {
        stateText = text;
        anim = animator;
        rogueScript = parentScript;
    }

    public override TaskStatus Run()
    {
        stateText.text = "ThrowSmoke";
        
        anim.Play("Throw");
        rogueScript.SpawnSmokeBomb();

        return TaskStatus.Success;
    }
}
