using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The idle node should be active when:

    The distance between the friendly agent and the player is not larger than 5 units. And the player is not spotted by the enemy agent. (or if the player escaped)
*/

public class BTIdle : BTBaseNode
{
    public override TaskStatus Run()
    {
        return TaskStatus.Failed;
    }
}
