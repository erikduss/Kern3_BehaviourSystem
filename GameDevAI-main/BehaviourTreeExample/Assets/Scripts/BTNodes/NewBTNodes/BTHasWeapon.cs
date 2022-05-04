using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BTHasWeapon : BTBaseNode
{
    private VariableFloat hasWeapon;
    private TextMesh stateText;

    public BTHasWeapon(VariableFloat _hasWeapon, TextMesh text)
    {
        hasWeapon = _hasWeapon;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "BTHasWeapon";

        if (hasWeapon != null && hasWeapon.Value > 0)
        {
            return TaskStatus.Success;
        }
        else
        {
            return TaskStatus.Failed;
        }
    }
}
