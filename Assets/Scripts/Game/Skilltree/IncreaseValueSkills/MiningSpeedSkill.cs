using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningSpeedSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.miningSpeed += increaseAmount;
    }
}
