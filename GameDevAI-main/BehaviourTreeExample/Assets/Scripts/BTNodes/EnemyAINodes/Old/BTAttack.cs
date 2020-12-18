using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The attack node should be active when:

    The player is in attack range and the agent has a weapon.
*/

public class BTAttack : BTBaseNode
{
    public override TaskStatus Run()
    {
        return TaskStatus.Failed;
    }
}
