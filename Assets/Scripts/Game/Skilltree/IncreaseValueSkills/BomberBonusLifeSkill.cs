using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBonusLifeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.bomberBonusLife += increaseAmount;
    }
}
