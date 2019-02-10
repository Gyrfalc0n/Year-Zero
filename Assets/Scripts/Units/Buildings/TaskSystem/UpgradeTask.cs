using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTask : Task
{
    ConstructedUnit previousBuilding;
    ConstructedUnit nextBuilding;

    public void Init(ConstructedUnit previous, ConstructedUnit next)
    {
        previousBuilding = previous;
        nextBuilding = next;
        active = true;
        remainingTime = next.GetRequiredTime();
        requiredTime = next.GetRequiredTime();
    }

    public override void OnFinishedTask()
    {
        base.OnFinishedTask();
        InstanceManager.instanceManager.InstantiateUnit(nextBuilding.GetPath(), previousBuilding.transform.position, Quaternion.identity);
        previousBuilding.KillUnit();
    }
    public override void Cancel()
    {
        PlayerManager.playerManager.PayBack(nextBuilding.costs, nextBuilding.pop);
        base.Cancel();
    }
}
