using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTEnemySequence : BTBaseNode
{
    private BTBaseNode[] nodes;
    public BTEnemySequence(params BTBaseNode[] inputNodes)
    {
        nodes = inputNodes;
    }

    public override TaskStatus Run()
    {
        foreach (BTBaseNode node in nodes)
        {
            TaskStatus result = node.Run();
            switch (result)
            {
                case TaskStatus.Failed: return TaskStatus.Failed;
                case TaskStatus.Success: continue;
                case TaskStatus.Running: return TaskStatus.Running;
            }
        }
        return TaskStatus.Success;
    }
}
