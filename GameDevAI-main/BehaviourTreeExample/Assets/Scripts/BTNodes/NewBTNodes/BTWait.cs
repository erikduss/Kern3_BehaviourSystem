using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTWait : BTBaseNode
{
    private float waitTime;
    private float _waitCounter = 0f;
    private TextMesh stateText;

    private bool waiting = false;

    public BTWait(float _waitTime, TextMesh text)
    {
        waitTime = _waitTime;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "Wait";

        _waitCounter += Time.deltaTime;

        if (!waiting)
        {
            waiting = true;
        }

        if (_waitCounter >= waitTime)
        {
            _waitCounter = 0;
            waiting = false;

            return TaskStatus.Success;
        }
        else if (_waitCounter < waitTime)
        {
            return TaskStatus.Running;
        }

        return TaskStatus.Failed;
    }
}
