using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MovableUnit
{
    public override void Awake()
    {
        maxLife = defaultMaxLife + (int)(defaultMaxLife * SkilltreeManager.manager.destroyerBonusLife);
        base.Awake();
    }

    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.destroyer;
    }
}
