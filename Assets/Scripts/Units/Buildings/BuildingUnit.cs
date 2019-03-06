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

    public override Vector3 GetSelectionCirclePos()
    {
        return new Vector3(0, -GetComponent<BoxCollider>().size.y / 2 + 0.01f, 0);
        //return new Vector3(0, 0.01f, 0);
    }
}
