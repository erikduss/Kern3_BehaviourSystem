using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTIsAware : BTBaseNode
{

    private Player playerScript;

    private TextMesh stateText;

    public BTIsAware(TextMesh text)
    {
        stateText = text;
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public override TaskStatus Run()
    {
        stateText.text = "BTIsAware";

        if (playerScript != null)
        {
            if (playerScript.isBeingChased)
            {
                return TaskStatus.Success;
            }
            else return TaskStatus.Failed;
        }
        return TaskStatus.Failed;
    }
}
