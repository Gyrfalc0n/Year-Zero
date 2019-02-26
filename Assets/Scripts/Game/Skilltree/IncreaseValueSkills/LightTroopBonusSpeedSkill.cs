using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroopBonusSpeedSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.lightTroopBonusSpeed += increaseAmount;
    }
}
