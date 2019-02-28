using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarRangeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.radarRange += increaseAmount;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<Radar>() != null)
            {
                Vector3 tmp = obj.fovCollider.GetRange();
                tmp.x =  SkilltreeManager.manager.radarRange;
                tmp.z = SkilltreeManager.manager.radarRange;
                obj.fovCollider.SetRange(tmp);
            }
        }
    }
}
