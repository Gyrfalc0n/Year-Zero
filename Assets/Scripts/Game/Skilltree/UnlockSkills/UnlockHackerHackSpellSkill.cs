using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockHackerHackSpellSkill : UnlockSkill
{
    public override void Effect()
    {
        Unlock(ref SkilltreeManager.manager.hackerHackSpell);
    }
}
