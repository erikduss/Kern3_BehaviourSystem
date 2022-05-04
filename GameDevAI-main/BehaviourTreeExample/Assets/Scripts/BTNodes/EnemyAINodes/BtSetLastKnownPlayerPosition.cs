using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTSetLastKnownPlayerPosition : BTBaseNode
{
    private VariableGameObject lastKnownPos;

    private GameObject detectedObject;

    private TextMesh stateText;

    public BTSetLastKnownPlayerPosition(VariableGameObject _lastKnownPos, GameObject _detectedObject,  TextMesh text)
    {
        lastKnownPos = _lastKnownPos;
        stateText = text;
        detectedObject = _detectedObject;
    }

    public override TaskStatus Run()
    {
        stateText.text = "BTSetLastKnownPlayerPosition";

        if (detectedObject != null)
        {
            lastKnownPos.Value.transform.position = detectedObject.gameObject.transform.position;
            return TaskStatus.Success;
        }
        return TaskStatus.Failed;
    }
}
