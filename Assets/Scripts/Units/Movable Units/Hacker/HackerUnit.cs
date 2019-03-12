using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerUnit : MovableUnit
{
    public override void Start()
    {
        agent.speed = speed + (int)(defaultSpeed * SkilltreeManager.manager.hackerBonusSpeed);
        base.Start();
    }

    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.hacker;
    }
}
