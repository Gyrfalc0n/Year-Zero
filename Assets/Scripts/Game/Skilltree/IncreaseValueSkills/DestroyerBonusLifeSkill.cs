using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBonusLifeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.destroyerBonusLife += increaseAmount;
    }
}
