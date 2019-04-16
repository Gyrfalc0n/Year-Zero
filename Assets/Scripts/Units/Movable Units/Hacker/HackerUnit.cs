using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerUnit : MovableUnit
{
    public override void Init2()
    {
        if (botIndex == -1)
            agent.speed = speed + (int)(defaultSpeed * SkilltreeManager.manager.hackerBonusSpeed);
        base.Init2();
    }

    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.hacker;
    }
}
