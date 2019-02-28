using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberSpeedSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.bomberProductionSpeed += increaseAmount;
    }
}
