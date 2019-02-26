using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerVisionSkill : IncreaseValueSkill
{
    public override void Effect()
    {
        SkilltreeManager.manager.hackerVisionSpell += increaseAmount;
    }
}
