using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreaseValueSkill : Skill
{
    [SerializeField]
    protected float increaseAmount;

    protected void Increase(ref float skill)
    {
        skill += increaseAmount;
    }
}
