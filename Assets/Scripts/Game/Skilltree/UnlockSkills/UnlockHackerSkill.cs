﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockHackerSkill : UnlockSkill
{
    public override void Effect()
    {
        Unlock(ref SkilltreeManager.manager.hacker);
    }
}