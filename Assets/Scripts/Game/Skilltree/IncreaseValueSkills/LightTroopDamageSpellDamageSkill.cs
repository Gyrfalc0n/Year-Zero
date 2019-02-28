using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroopDamageSpellDamageSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.lightTroopDamageSpellDamage += increaseAmount;
    }
}
