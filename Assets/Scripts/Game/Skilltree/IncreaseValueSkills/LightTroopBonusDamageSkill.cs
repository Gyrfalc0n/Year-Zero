using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroopBonusDamageSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.lightTroopBonusDamage += increaseAmount;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<LightTroop>() != null)
            {
                obj.GetComponent<LightTroop>().damage += (int)(obj.GetComponent<LightTroop>().defaultDamage * SkilltreeManager.manager.lightTroopBonusDamage);
            }
        }
    }
}
