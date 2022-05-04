using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTDebug : BTBaseNode
{
    private string debugString;

    public BTDebug(string _debugString)
    {
        debugString = _debugString;
    }

    public override TaskStatus Run()
    {
        Debug.Log(debugString);

        return TaskStatus.Success;
    }
}