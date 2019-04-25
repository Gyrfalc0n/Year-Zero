using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBonusDamageSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        Increase(ref SkilltreeManager.manager.bomberBonusDamage);
        UpdateDamagesAssociatedUnits<Bomber>(ref SkilltreeManager.manager.bomberBonusDamage);
    }
}
