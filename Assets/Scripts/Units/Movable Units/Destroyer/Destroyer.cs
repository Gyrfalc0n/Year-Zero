using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MovableUnit
{
    public override void InitUnit(int botIndex)
    {
        if (botIndex == -1)
            maxLife = defaultMaxLife + (int)(defaultMaxLife * SkilltreeManager.manager.destroyerBonusLife);
        base.InitUnit(botIndex);
        GetComponentInChildren<SupportSpellZone>().Init(spellHolder);
    }

    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.destroyer;
    }
}
