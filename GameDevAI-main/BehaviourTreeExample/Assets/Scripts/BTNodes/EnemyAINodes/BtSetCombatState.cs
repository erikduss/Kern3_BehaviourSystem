using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTSetCombatState : BTBaseNode
{
    private bool status;

    private Player playerScript;

    private TextMesh stateText;

    public BTSetCombatState(bool _status, TextMesh text)
    {
        status = _status;
        stateText = text;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public override TaskStatus Run()
    {
        stateText.text = "BTSetCombatState";

        if (playerScript != null)
        {
            playerScript.isBeingChased = status;
            return TaskStatus.Success;
        }
        return TaskStatus.Failed;
    }
}
