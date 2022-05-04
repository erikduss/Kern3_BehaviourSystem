using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTIsSpotted : BTBaseNode
{
    private Player playerScript;

    private TextMesh stateText;

    public BTIsSpotted(Player script, TextMesh text)
    {
        stateText = text;
        playerScript = script;
    }

    public override TaskStatus Run()
    {
        stateText.text = "BTIsSpotted";

        if(playerScript != null)
        {
            if (playerScript.isBeingChased)
            {
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failed;
    }
}
