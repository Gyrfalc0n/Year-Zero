using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockDestroyerBannerSkill : UnlockSkill
{
    public override void Effect()
    {
        Unlock(ref SkilltreeManager.manager.destroyerBanner);
    }
}
