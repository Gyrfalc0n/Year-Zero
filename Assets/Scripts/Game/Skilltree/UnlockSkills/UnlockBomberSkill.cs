using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockBomberSkill : UnlockSkill
{
    public override void Effect()
    {
        Unlock(ref SkilltreeManager.manager.bomber);
    }
}
