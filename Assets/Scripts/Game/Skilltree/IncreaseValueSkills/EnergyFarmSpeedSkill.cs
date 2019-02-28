using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyFarmSpeedSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.energyFarmSpeed += increaseAmount;
    }
}
