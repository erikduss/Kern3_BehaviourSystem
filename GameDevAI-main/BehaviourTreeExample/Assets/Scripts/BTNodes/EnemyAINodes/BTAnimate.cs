using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAnimate : BTBaseNode
{
    private Animator animator;
    private string animation;

    public BTAnimate(Animator anim, string clipName)
    {
        animator = anim;
        animation = clipName;
    }

    public override TaskStatus Run()
    {
        if(animator != null && animation != null)
        {
            animator.Play(animation);
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failed;
        }
    }
}
