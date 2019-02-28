using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberMissileSpell : Spell
{
    float time;
    float maxTime = 5;

    float tmpDamage;

    public override void Start()
    {
        base.Start();
        time = 0;
        needSpellControls = false;
    }

    public override void Effect()
    {
        time = maxTime;
        tmpDamage = associatedUnit.GetComponent<Bomber>().damage;
        associatedUnit.GetComponent<Bomber>().damage = associatedUnit.GetComponent<Bomber>().damage * 1.5f * SkilltreeManager.manager.bomberMissileSpell;
        associatedUnit.GetComponent<CombatSystem>().attackRate *= 2;
    }

    public void Deffect()
    {
        associatedUnit.GetComponent<LightTroop>().damage = tmpDamage;
        associatedUnit.GetComponent<CombatSystem>().attackRate /= 2;
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
        return SkilltreeManager.manager.bomberMissileSpell > 0;
    }
}
