using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMissileSpell : Spell
{
    float time;
    float maxTime = 5;

    public override void Start()
    {
        base.Start();
        time = 0;
        needSpellControls = false;
    }

    public override void Effect()
    {
        time = maxTime;
        //increase missile per second
        throw new System.NotImplementedException();
    }

    public override void Update()
    {
        base.Update();
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                //Decrease
            }
        }
    }

    public override bool IsUnlocked()
    {
        return SkilltreeManager.manager.bomberMissileSpell > 0;
    }
}
