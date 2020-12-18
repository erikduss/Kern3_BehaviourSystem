using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The follow node should be active when:

    The distance between the friendly agent and the player is bigger than 5 units and the player is not in combat.
*/

public class BTFollow : BTBaseNode
{
    public override TaskStatus Run()
    {
        return TaskStatus.Failed;
    }
}
