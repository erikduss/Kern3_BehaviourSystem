using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The search cover node should be active when:

    The enemy agent is chasing (or attacking) the player and the friendly agent has not found cover yet.
*/

public class BTSearchCover : BTBaseNode
{
    public override TaskStatus Run()
    {
        Debug.Log("Covering");
        return TaskStatus.Failed;
    }
}
