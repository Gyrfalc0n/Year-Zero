using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroopBonusLifeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.lightTroopBonusLife += increaseAmount;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<LightTroop>() != null)
            {
                obj.GetComponent<LightTroop>().maxLife = (int)(obj.GetComponent<LightTroop>().defaultMaxLife * SkilltreeManager.manager.lightTroopBonusLife);
            }
        }
    }
}
