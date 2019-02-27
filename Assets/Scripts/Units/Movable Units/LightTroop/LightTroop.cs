using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroop : MovableUnit
{
    public override void Awake()
    {
        maxLife = defaultMaxLife + (int)(defaultMaxLife * SkilltreeManager.manager.lightTroopBonusLife);
        agent.speed = speed + (int)(defaultSpeed * SkilltreeManager.manager.lightTroopBonusSpeed);
        base.Awake();
    }

    public override bool IsAvailable()
    {
        return (SkilltreeManager.manager.lightTroop);
    }
}
