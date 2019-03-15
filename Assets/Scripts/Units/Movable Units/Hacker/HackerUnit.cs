using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerUnit : MovableUnit
{
    public override void Start()
    {
        if (botIndex == -1)
            agent.speed = speed + (int)(defaultSpeed * SkilltreeManager.manager.hackerBonusSpeed);
        base.Start();
    }

    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.hacker;
    }
}
