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
        PlayerManager.playerManager.AddWood(associatedUnit.costs[0]);
        PlayerManager.playerManager.AddStone(associatedUnit.costs[1]);
        PlayerManager.playerManager.AddGold(associatedUnit.costs[2]);
        PlayerManager.playerManager.AddMeat(associatedUnit.costs[3]);
        PlayerManager.playerManager.AddPopulation(associatedUnit.costs[4]);
        base.Cancel();
    }
}
