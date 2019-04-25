using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberBonusLifeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.bomberBonusLife += increaseAmount;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<Bomber>() != null)
            {
                obj.GetComponent<Bomber>().maxLife = (int)(obj.GetComponent<Bomber>().defaultMaxLife * SkilltreeManager.manager.bomberBonusLife);
            }
        }
    }
}
