using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSequence : BTBaseNode
{
    private int index = 0;
    private BTBaseNode[] nodes;
    public BTSequence(params BTBaseNode[] inputNodes)
    {
        nodes = inputNodes;
    }

    public override TaskStatus Run()
    {
        for (; index < nodes.Length; index++)
        {
            switch (nodes[index].Run())
            {
                case TaskStatus.Failed: return TaskStatus.Failed;
                case TaskStatus.Success: continue;
                case TaskStatus.Running: return TaskStatus.Running;
            }
        }
        index = 0;
        return TaskStatus.Success;
    }
}
