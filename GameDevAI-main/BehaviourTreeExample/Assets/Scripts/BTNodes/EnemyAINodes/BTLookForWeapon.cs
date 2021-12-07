using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTLookForWeapon : BTBaseNode
{
    private Transform[] weapons;
    private VariableGameObject target;
    private NavMeshAgent curagent;
    private bool guardHasWeapon;

    public BTLookForWeapon(Transform[] _weapons, VariableGameObject targ, NavMeshAgent agent, bool hasWeapon)
    {
        weapons = _weapons;
        target = targ;
        curagent = agent;
        guardHasWeapon = hasWeapon;
    }

    public override TaskStatus Run()
    {
        if(weapons != null)
        {
            if (!guardHasWeapon)
            {
                target.Value = FindClosestWeapon().gameObject;
                guardHasWeapon = true;
                return TaskStatus.Success;
            }
            else
            {
                target.Value = GameObject.FindGameObjectWithTag("Player");

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
