using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelDamageSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        Increase(ref SkilltreeManager.manager.turrelBonusDamage);
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<Turrel>() != null)
            {
                obj.GetComponentInChildren<TurrelFOV>().damage = obj.GetComponentInChildren<TurrelFOV>().defaultDamage * SkilltreeManager.manager.turrelBonusDamage;
            }
        }
    }
}
