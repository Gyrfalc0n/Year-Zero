using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelDamageSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.turrelDamage += increaseAmount;
    }
}
