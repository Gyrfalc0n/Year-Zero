using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LightTroopBonusSpeedSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.lightTroopBonusSpeed += increaseAmount;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<LightTroop>() != null)
            {
                obj.GetComponent<NavMeshAgent>().speed += (int)(obj.GetComponent<LightTroop>().defaultSpeed * SkilltreeManager.manager.lightTroopBonusSpeed);
            }
        }
    }
}
