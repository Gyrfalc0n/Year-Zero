using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSpeedSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.constructionSpeed += increaseAmount;
    }
}
