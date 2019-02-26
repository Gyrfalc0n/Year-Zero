using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurrelRangeSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.turrelRange += increaseAmount;
    }
}
