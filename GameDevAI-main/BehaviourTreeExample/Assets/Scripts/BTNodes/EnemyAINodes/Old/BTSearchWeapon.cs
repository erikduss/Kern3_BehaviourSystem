using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The search weapon node should be active when:

    The agent sees the player and has no weapon.
*/

public class BTSearchWeapon : BTBaseNode
{
    public override TaskStatus Run()
    {
        return TaskStatus.Failed;
    }
}
