using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTask : Task
{
    public void Init()
    {
        //memorize what to upgrade and costs
    }

    public override void OnFinishedTask()
    {
        base.OnFinishedTask();
        //Upgrade
        //Check upgrade
    }

    public override void Cancel()
    {
        base.Cancel();
        //Check Upgrade
    }
}
