using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAnimate : BTBaseNode
{
    public BTAnimate(Animator anim, string clipName)
    {

    }

    public override TaskStatus Run()
    {
        return TaskStatus.Failed;
    }
}
