using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMissileSpellSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.bomberMissileSpell += increaseAmount;
    }
}
