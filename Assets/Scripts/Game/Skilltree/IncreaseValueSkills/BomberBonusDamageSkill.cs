using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBonusDamageSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.bomberBonusDamage += increaseAmount;
    }
}
