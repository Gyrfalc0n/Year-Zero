using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBonusLifeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.destroyerBonusLife += increaseAmount;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<Destroyer>() != null)
            {
                obj.GetComponent<Destroyer>().maxLife = (int)(obj.GetComponent<Destroyer>().defaultMaxLife * SkilltreeManager.manager.lightTroopBonusLife);
            }
        }
    }
}
