using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTLookForWeaponOld : BTBaseNode
{
    private Transform[] weapons;
    private VariableGameObject target;
    private NavMeshAgent curagent;
    private VariableFloat guardHasWeapon;
    private VariableGameObject lastKnownPlayerPosition;
    private bool isChasing;

    private TextMesh stateText;

    public BTLookForWeaponOld(Transform[] _weapons, VariableGameObject targ, NavMeshAgent agent, VariableFloat hasWeapon, VariableGameObject lastKnownPos, bool chasing, TextMesh text)
    {
        weapons = _weapons;
        target = targ;
        curagent = agent;
        guardHasWeapon = hasWeapon;
        lastKnownPlayerPosition = lastKnownPos;
        isChasing = chasing;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "LookForWeapon";

        if (guardHasWeapon.Value < 1f)
        {
            if (weapons != null)
            {
                if (curagent != null && target != null)
                {
                    target.Value = FindClosestWeapon().gameObject;
                    curagent.destination = target.Value.transform.position;
                    if (curagent.remainingDistance < 0.1f)
                    {
                        guardHasWeapon.Value = 1f;
                        target.Value = lastKnownPlayerPosition.Value;
                        return TaskStatus.Success;
                    }
                    return TaskStatus.Running;
                }
            }
            return TaskStatus.Failed;
        }
        else if (!isChasing)
        {
            target.Value = lastKnownPlayerPosition.Value;
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Success;
        }
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
