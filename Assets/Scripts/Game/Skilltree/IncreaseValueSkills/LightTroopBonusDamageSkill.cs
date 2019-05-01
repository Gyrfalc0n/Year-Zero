using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroopBonusDamageSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        Increase(ref SkilltreeManager.manager.lightTroopBonusDamage);
        UpdateDamagesAssociatedUnits<LightTroop>(ref SkilltreeManager.manager.lightTroopBonusDamage);
    }
}
