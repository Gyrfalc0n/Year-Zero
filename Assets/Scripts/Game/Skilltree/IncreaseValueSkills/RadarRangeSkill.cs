using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarRangeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.radarRange += increaseAmount;
    }
}
