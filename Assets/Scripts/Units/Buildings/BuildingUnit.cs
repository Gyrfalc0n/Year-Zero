using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingUnit : DestructibleUnit
{
    public virtual void Cancel() { }

    public virtual float GetCurrentActionAdvancement()
    {
        return -42f;
    }
}
