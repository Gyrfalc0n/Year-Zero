using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelRangeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.turrelRange += increaseAmount;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<Turrel>() != null)
            {
                obj.GetComponentInChildren<TurrelFOV>().SetRange(SkilltreeManager.manager.turrelRange);
            }
        }
    }
}
