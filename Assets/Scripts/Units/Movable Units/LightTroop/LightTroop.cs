using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroop : MovableUnit
{
    public override void InitUnit(int botIndex)
    {
        base.InitUnit(botIndex);
        if (botIndex == -1)
        {
            maxLife = (int)(defaultMaxLife * SkilltreeManager.manager.lightTroopBonusLife);
            agent.speed = (int)(defaultSpeed * SkilltreeManager.manager.lightTroopBonusSpeed);
            damage = (int)(defaultDamage * SkilltreeManager.manager.lightTroopBonusDamage);
        }
    }

    public override bool IsAvailable()
    {
        return (SkilltreeManager.manager.lightTroop);
    }
}
