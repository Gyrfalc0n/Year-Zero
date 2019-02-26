using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.radar += increaseAmount;
    }
}
