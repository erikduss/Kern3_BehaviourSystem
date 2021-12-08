using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTAnimate : BTBaseNode
{
    private Animator animator;
    private string animation;
    private TextMesh stateText;

    public BTAnimate(Animator anim, string clipName, TextMesh text)
    {
        animator = anim;
        animation = clipName;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "Animate";

        if (animator != null && animation != null)
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
