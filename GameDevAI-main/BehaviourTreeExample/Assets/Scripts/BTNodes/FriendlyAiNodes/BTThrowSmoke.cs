using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    The thow smoke node should be active when:

    The friendly agent found cover and the player is still getting chases/attacked.
*/

public class BTThrowSmoke : BTBaseNode
{
    public override TaskStatus Run()
    {
        return TaskStatus.Failed;
    }
}
