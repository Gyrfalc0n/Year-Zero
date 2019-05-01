using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroopBonusLifeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        Increase(ref SkilltreeManager.manager.lightTroopBonusLife);
        UpdateLifeAssociatedUnits<LightTroop>(ref SkilltreeManager.manager.lightTroopBonusLife);
    }
}
