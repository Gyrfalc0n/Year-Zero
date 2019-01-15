using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InstantiateTask : Task
{
    string unit;

    public void Init(MovableUnit unit, string path, float requiredTime)
    {
        costs = unit.costs;
        this.unit = path;
        this.requiredTime = requiredTime;
        remainingTime = requiredTime;
    }

    public override void OnFinishedTask()
    {
        base.OnFinishedTask();
        PhotonNetwork.Instantiate(unit, new Vector3(1,1,1), Quaternion.identity);
    }
}
