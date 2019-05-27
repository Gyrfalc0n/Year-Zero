using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MovableUnit
{
    public override void InitUnit(int botIndex)
    {
        if (botIndex == -1)
            maxLife = (int)(defaultMaxLife * SkilltreeManager.manager.destroyerBonusLife);
        base.InitUnit(botIndex);
        GetComponentInChildren<SupportSpellZone>().Init(spellHolder);
    }

    public override Vector3 GetSelectionCircleSize()
    {
        return new Vector3(3, 3, 3);
    }

    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.destroyer;
    }
}
