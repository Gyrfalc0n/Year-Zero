using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroopDamageSpell : Spell
{
    public override void Start()
    {
        base.Start();
        needSpellControls = false;
    }

    public override void Effect()
    {
        throw new System.NotImplementedException();
    }
}
