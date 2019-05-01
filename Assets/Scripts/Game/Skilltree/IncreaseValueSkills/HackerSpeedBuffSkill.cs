using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HackerSpeedBuffSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        Increase(ref SkilltreeManager.manager.hackerBonusSpeed);
        UpdateSpeedAssociatedUnits<HackerUnit>(ref SkilltreeManager.manager.hackerBonusSpeed);
    }
}
