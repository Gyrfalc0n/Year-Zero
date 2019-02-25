using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTroopBonusLifeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        Increase(ref SkilltreeManager.manager.basicTroopBonusLife);
    }
}
