using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLightTroopDamageSpellSkill : UnlockSkill
{
    public override void Effect()
    {
        Unlock(ref SkilltreeManager.manager.lightTroopDamageSpell);
    }
}
