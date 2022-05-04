using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTCanSeeObject : BTBaseNode
{
    private GameObject detectionTarget;
    private GameObject origin;
    private float maxRange;
    private float fov;
    private TextMesh stateText;

    public BTCanSeeObject(GameObject _obj, GameObject _origin, float _maxRange, float _fov, TextMesh text)
    {
        detectionTarget = _obj;
        origin = _origin;
        maxRange = _maxRange;
        fov = _fov;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "BTCanSeeObject";

        if (IsObjectInFOV())
        {
            Vector3 direction = (detectionTarget.transform.position - origin.transform.position).normalized;

            RaycastHit hit;

            if ((Physics.Raycast(origin.transform.position, direction, out hit, maxRange)))
            {
                if (hit.transform.gameObject == detectionTarget)
                {
                    return TaskStatus.Success;
                }
                else
                {
                    return TaskStatus.Failed;
                }
            }
        }

        return TaskStatus.Failed;
    }

    // check to see if the object is in the current fov
    private bool IsObjectInFOV()
    {
        Vector3 targetDir = detectionTarget.transform.position - origin.transform.position;
        Vector3 forward = origin.transform.forward;
        float angle = Vector3.Angle(targetDir, forward);
        if (angle < fov)
            return true;
        else return false;
    }
}
