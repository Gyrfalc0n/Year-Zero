﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HackerUnit : MovableUnit
{
    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.hacker;
    }
}
