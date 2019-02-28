using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turrel : ConstructedUnit
{
    public override bool IsAvailable()
    {
        return SkilltreeManager.manager.turrel;
    }
}
