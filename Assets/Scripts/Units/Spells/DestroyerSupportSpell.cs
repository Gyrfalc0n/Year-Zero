using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerSupportSpell : Spell
{
    float time;
    float maxTime = 5;

    bool activated;

    public override void Start()
    {
        base.Start();
        needSpellControls = false;
    }

    public override void Effect()
    {
        time = maxTime;
        activated = true;
    }

    public override void Update()
    {
        base.Update();
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                activated = false;
            }
        }
    }

    public bool Activated()
    {
        return activated;
    }
}
