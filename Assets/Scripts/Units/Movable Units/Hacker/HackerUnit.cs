using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerUnit : MovableUnit
{
    public override void Awake()
    {
        agent.speed = speed + (int)(defaultSpeed * SkilltreeManager.manager.hackerBonusSpeed);
        base.Awake();
    }

    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.hacker;
    }
}
