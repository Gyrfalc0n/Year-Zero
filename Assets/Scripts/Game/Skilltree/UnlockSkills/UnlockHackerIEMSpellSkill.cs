using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockHackerIEMSpellSkill : UnlockSkill
{
    public override void Effect()
    {
        Unlock(ref SkilltreeManager.manager.hackerIEMSpell);
    }
}
