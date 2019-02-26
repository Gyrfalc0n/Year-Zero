using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroopBonusDamageSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.lightTroopBonusDamage += increaseAmount;
    }
}
