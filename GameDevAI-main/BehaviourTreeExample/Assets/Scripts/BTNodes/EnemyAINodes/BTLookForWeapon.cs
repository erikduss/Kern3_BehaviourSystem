using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTLookForWeapon : BTBaseNode
{
    private Transform[] weapons;
    private VariableGameObject target;
    private NavMeshAgent curagent;

    private TextMesh stateText;

    public BTLookForWeapon(Transform[] _weapons, VariableGameObject targ, NavMeshAgent agent, TextMesh text)
    {
        weapons = _weapons;
        target = targ;
        curagent = agent;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "LookForWeapon";

        if (weapons != null)
        {
            if (curagent != null && target != null)
            {
                target.Value = FindClosestWeapon().gameObject;
                return TaskStatus.Success;
            }
        }
        return TaskStatus.Failed;
    }

    private Transform FindClosestWeapon()
    {
        Transform closestWeapon = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = curagent.transform.position;
        foreach (Transform t in weapons)
        {
            float dist = Vector3.Distance(t.position, currentPos);
            if (dist < minDist)
            {
                closestWeapon = t;
                minDist = dist;
            }
        }
        return closestWeapon;
    }
}
