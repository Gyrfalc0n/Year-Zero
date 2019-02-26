using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockTurrelSkill : UnlockSkill
{
    public override void Effect()
    {
        Unlock(ref SkilltreeManager.manager.turrel);
    }
}
