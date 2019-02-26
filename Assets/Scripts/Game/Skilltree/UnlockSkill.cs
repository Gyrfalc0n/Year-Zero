using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockSkill : Skill
{
    protected void Unlock(ref bool skill)
    {
        skill = true;
    }
}
