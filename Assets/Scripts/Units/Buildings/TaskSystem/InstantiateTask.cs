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
        remainingTime = unit.GetRequiredTime();
        requiredTime = unit.GetRequiredTime();
    }

    public override void OnFinishedTask()
    {
        base.OnFinishedTask();
        print(MyTools.GetPath(associatedUnit.gameObject));
        GameObject unit = InstanceManager.instanceManager.InstantiateUnit(MyTools.GetPath(associatedUnit.gameObject), associatedBuilding.GetComponent<ProductionBuilding>().GetSpawnPointCoords(), Quaternion.identity);
        //GameObject unit = InstanceManager.instanceManager.InstantiateUnit(associatedUnit.GetPath(), associatedBuilding.GetComponent<ProductionBuilding>().GetSpawnPointCoords(), Quaternion.identity);
        unit.GetComponent<MovableUnit>().Init(associatedBuilding.GetComponent<ProductionBuilding>().GetBannerCoords());
    }
    public override void Cancel()
    {
        PlayerManager.playerManager.PayBack(associatedBuilding.costs);
        base.Cancel();
    }
}
