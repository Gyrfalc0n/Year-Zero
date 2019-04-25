using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBonusLifeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        Increase(ref SkilltreeManager.manager.destroyerBonusLife);
        UpdateLifeAssociatedUnits<Destroyer>(ref SkilltreeManager.manager.destroyerBonusLife);
    }
}
