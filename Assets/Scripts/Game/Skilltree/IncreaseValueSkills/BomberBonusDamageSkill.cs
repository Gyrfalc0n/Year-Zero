using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBonusDamageSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.bomberBonusDamage += increaseAmount;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<Bomber>() != null)
            {
                obj.GetComponent<Bomber>().damage += (int)(obj.GetComponent<Bomber>().defaultDamage * SkilltreeManager.manager.bomberBonusDamage);
            }
        }
    }
}
