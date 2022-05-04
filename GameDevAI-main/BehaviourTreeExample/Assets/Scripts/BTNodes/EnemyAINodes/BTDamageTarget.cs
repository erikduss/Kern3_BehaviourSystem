using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BTDamageTarget : BTBaseNode
{
    private VariableGameObject target;
    private GameObject attacker;
    private int damage;
    private TextMesh stateText;

    public BTDamageTarget(VariableGameObject _target, GameObject _attacker, int _damage, TextMesh text)
    {
        damage = _damage;
        attacker = _attacker;
        target = _target;
        stateText = text;
    }

    public override TaskStatus Run()
    {
        stateText.text = "BTDamageTarget";

        if(target != null)
        {
            try
            {
                IDamageable objectToDamage = target.Value.GetComponent<IDamageable>();
                objectToDamage.TakeDamage(attacker, damage);
                return TaskStatus.Success;
            }
            catch
            {
                return TaskStatus.Failed;
            }
        }
        return TaskStatus.Failed;
    }
}
