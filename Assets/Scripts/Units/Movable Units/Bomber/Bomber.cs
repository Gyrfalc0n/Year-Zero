using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomber : MovableUnit
{
    public override void Awake()
    {
        maxLife = defaultMaxLife + (int)(defaultMaxLife * SkilltreeManager.manager.bomberBonusLife);
        base.Awake();
    }

    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.bomber;
    }

    public override float GetRequiredTime()
    {
        return base.GetRequiredTime() * SkilltreeManager.manager.bomberProductionSpeed;
    }
}
