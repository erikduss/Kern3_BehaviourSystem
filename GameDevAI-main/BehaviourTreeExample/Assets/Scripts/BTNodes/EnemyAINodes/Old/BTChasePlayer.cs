using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/*
    The chase player node should be active when:

    1. The agent sees the player and he already has a weapon.
    2. The agent found the weapon and is still close enough to the player.
*/

public class BTChasePlayer : BTBaseNode
{
    private bool seesPlayer;
    private VariableFloat isArmed;
    private VariableGameObject target;
    private TextMesh stateText;

    public BTChasePlayer(bool playerInSight, VariableFloat hasWeapon, VariableGameObject targ, TextMesh text)
    {
        seesPlayer = playerInSight;
        isArmed = hasWeapon;
        target = targ;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "ChasePlayer";

        if(isArmed.Value > 0 && seesPlayer)
        {
            target.Value = GameObject.FindGameObjectWithTag("Player");
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failed;
        }
    }
}
