using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerSpeedBuffSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.hackerBonusSpeed += increaseAmount;
    }
}
