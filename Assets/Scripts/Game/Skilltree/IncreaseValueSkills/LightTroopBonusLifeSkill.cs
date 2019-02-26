using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroopBonusLifeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.lightTroopBonusLife += increaseAmount;
    }
}
