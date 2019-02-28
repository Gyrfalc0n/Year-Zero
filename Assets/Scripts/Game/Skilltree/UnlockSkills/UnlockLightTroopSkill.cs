using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockLightTroopSkill : UnlockSkill
{
    public override void Effect()
    {
        Unlock(ref SkilltreeManager.manager.lightTroop);
    }
}
