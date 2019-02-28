using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightTroopDamageSpell : Spell
{
    float time;
    float maxTime = 5;

    public override void Start()
    {
        base.Start();
        needSpellControls = false;
    }

    public override void Effect()
    {
        time = maxTime;
        associatedUnit.GetComponent<LightTroop>().damage = associatedUnit.GetComponent<LightTroop>().damage * 1.5f * SkilltreeManager.manager.lightTroopDamageSpellDamage;
    }

    public void Deffect()
    {
        associatedUnit.GetComponent<LightTroop>().damage = associatedUnit.GetComponent<LightTroop>().damage / 1.5f / SkilltreeManager.manager.lightTroopDamageSpellDamage;
    }

    public override void Update()
    {
        base.Update();
        if (time > 0)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                Deffect();
            }
        }
    }

    public override bool IsUnlocked()
    {
        return SkilltreeManager.manager.lightTroopDamageSpell;
    }
}
