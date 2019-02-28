using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaboratorySpeedSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.laboratorySpeed += increaseAmount;
    }
}
