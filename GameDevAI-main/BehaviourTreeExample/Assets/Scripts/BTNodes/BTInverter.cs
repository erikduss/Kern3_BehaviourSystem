using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTInverter : BTBaseNode
{
    private BTBaseNode nodeToInvert;

    public BTInverter(BTBaseNode node)
    {
        nodeToInvert = node;
    }

    public override TaskStatus Run()
    {
        switch (nodeToInvert.Run())
        {
            case TaskStatus.Success:
                return TaskStatus.Failed;
            case TaskStatus.Failed:
                return TaskStatus.Success;
            default:
                return TaskStatus.Running;
        }
    }
}
