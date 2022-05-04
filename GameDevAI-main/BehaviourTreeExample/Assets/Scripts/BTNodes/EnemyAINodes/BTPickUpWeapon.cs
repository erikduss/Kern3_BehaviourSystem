using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTPickUpWeapon : BTBaseNode
{
    private VariableFloat hasWeapon;

    private TextMesh stateText;

    public BTPickUpWeapon(VariableFloat _hasWeapon, TextMesh text)
    {
        hasWeapon = _hasWeapon;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "BTPickUpWeapon";

        hasWeapon.Value = 1;

        if(hasWeapon.Value > 0)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Failed;
    }
}
