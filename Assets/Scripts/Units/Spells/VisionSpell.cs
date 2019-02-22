using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisionSpell : Spell
{
    VisionSpell()
    {
        requiredTimeBonus = 1;
        //requiredTimeBonus = SkilltreeManager.manager.
        //remainingTimeSpeedBonus = SkilltreeManager.manager.
    }

    public override void Effect()
    {
        print("Vision!!");
        //Effect
    }
}
