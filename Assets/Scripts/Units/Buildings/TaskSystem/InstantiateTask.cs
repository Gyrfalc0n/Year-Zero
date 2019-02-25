using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InstantiateTask : Task
{
    MovableUnit associatedUnit;

    public void Init(MovableUnit unit)
    {
        associatedUnit = unit;
        active = true;
        remainingTime = unit.GetMaxlife();
        requiredTime = unit.GetMaxlife();
    }

    public override void OnFinishedTask()
    {
        base.OnFinishedTask();
        GameObject unit = InstanceManager.instanceManager.InstantiateUnit(associatedUnit.GetPath(), associatedBuilding.GetComponent<ProductionBuilding>().GetSpawnPointCoords(), Quaternion.identity);
        unit.GetComponent<MovableUnit>().Init(associatedBuilding.GetComponent<ProductionBuilding>().GetBannerCoords());
    }
    public override void Cancel()
    {
        PlayerManager.playerManager.PayBack(associatedUnit.costs, associatedUnit.pop);
        base.Cancel();
    }
}
