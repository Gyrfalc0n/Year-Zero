using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IncreaseValueSkill : Skill
{
    [SerializeField]
    protected float increaseAmount;

    protected void Increase(ref float skill)
    {
        skill += increaseAmount;
    }

    protected void UpdateDamagesAssociatedUnits<T>(ref float skill)
    {
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<T>() != null)
            {
                obj.GetComponent<MovableUnit>().damage = (int)(obj.GetComponent<MovableUnit>().defaultDamage * skill);
            }
        }
    }

    protected void UpdateLifeAssociatedUnits<T>(ref float skill)
    {
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<T>() != null)
            {
                obj.GetComponent<MovableUnit>().maxLife = (int)(obj.GetComponent<MovableUnit>().defaultMaxLife * skill);
            }
        }
    }

    protected void UpdateSpeedAssociatedUnits<T>(ref float skill)
    {
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<T>() != null)
            {
                obj.GetComponent<NavMeshAgent>().speed = (int)(obj.GetComponent<MovableUnit>().defaultSpeed * skill);
            }
        }
    }
}
