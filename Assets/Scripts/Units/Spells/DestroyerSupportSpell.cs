using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerSupportSpell : Spell
{
    public override void Start()
    {
        base.Start();
        needSpellControls = false;
    }

    public override void Effect()
    {
        //Activate buff
        throw new System.NotImplementedException();
    }
}
