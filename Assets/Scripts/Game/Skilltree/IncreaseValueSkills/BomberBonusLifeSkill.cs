using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBonusLifeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        Increase(ref SkilltreeManager.manager.bomberBonusLife);
        UpdateLifeAssociatedUnits<Bomber>(ref SkilltreeManager.manager.bomberBonusLife);
    }
}
