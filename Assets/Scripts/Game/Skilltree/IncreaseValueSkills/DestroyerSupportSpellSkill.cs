using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerSupportSpellSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.destroyerSupportSpell += increaseAmount;
    }
}
