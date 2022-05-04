using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTSelector : BTBaseNode
{
    private int index = 0;
    private BTBaseNode[] nodes;
    public BTSelector(params BTBaseNode[] inputNodes)
    {
        nodes = inputNodes;
    }

    public override TaskStatus Run()
    {
        foreach(BTBaseNode node in nodes)
        {
            switch (node.Run())
            {
                case TaskStatus.Failed: continue;
                case TaskStatus.Success: continue;
                case TaskStatus.Running: return TaskStatus.Running;
            }
        }
        index = 0;
        return TaskStatus.Success;
    }
}
