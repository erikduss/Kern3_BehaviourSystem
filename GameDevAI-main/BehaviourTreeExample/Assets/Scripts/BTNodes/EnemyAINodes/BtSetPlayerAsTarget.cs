using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTSetPlayerAsTarget : BTBaseNode
{
    private VariableGameObject target;
    private NavMeshAgent curagent;
    private GameObject playerObject;

    private TextMesh stateText;

    public BTSetPlayerAsTarget(VariableGameObject targ, NavMeshAgent agent, GameObject _playerObject, TextMesh text)
    {
        target = targ;
        curagent = agent;
        playerObject = _playerObject;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "BTSetPlayerAsTarget";

        if (playerObject != null && curagent != null && target != null)
        {
            target.Value = playerObject;
            return TaskStatus.Success;
        }
        return TaskStatus.Failed;
    }
}
