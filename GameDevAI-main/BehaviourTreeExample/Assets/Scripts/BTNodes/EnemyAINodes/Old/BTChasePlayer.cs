using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The chase player node should be active when:

    1. The agent sees the player and he already has a weapon.
    2. The agent found the weapon and is still close enough to the player.
*/

public class BTChasePlayer : BTBaseNode
{
    public override TaskStatus Run()
    {
        return TaskStatus.Failed;
    }
}
