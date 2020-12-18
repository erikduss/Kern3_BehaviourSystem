using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The wander node should be active when:
    
    1. The game starts (initial state)
    2. The Distance to the player is bigger than 5 units (lost the player)
    3. The player is dead
*/

public class BTWander : BTBaseNode
{
    public override TaskStatus Run()
    {
        return TaskStatus.Failed;
    }
}
