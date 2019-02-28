using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HackerSpeedBuffSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.hackerBonusSpeed += increaseAmount;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<HackerUnit>() != null)
            {
                obj.GetComponent<NavMeshAgent>().speed += (int)(obj.GetComponent<HackerUnit>().defaultSpeed * SkilltreeManager.manager.hackerBonusSpeed);
            }
        }
    }
}
