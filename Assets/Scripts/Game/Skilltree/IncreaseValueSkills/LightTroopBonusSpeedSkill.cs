using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightTroopBonusSpeedSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        Increase(ref SkilltreeManager.manager.lightTroopBonusSpeed);
        UpdateSpeedAssociatedUnits<LightTroop>(ref SkilltreeManager.manager.lightTroopBonusSpeed);
    }
}
