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
        PhotonNetwork.Instantiate(associatedUnit.GetPath(), new Vector3(1,1,1), Quaternion.identity);
    }
    public override void Cancel()
    {
        PlayerManager.playerManager.PayBack(associatedBuilding.costs);
        base.Cancel();
    }
}
