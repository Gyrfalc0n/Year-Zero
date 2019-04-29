using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
        Destroyer destroyer = null;
        int i = 0;
        while (destroyer == null && i < InstanceManager.instanceManager.mySelectableObjs.Count)
        {
            if (InstanceManager.instanceManager.mySelectableObjs[i].GetComponent<Destroyer>() != null)
            {
                destroyer = InstanceManager.instanceManager.mySelectableObjs[i].GetComponent<Destroyer>();
            }
            i++;
        }

        if (destroyer != null)
        {
            GameObject unit = InstanceManager.instanceManager.InstantiateUnit(associatedUnit.GetPath(), destroyer.transform.position + new Vector3 (-5,0,0), Quaternion.identity, associatedBuilding.botIndex);
            unit.GetComponent<MovableUnit>().Init(destroyer.transform.position + new Vector3(-10,0,0));
        }
        else
        {
            GameObject unit = InstanceManager.instanceManager.InstantiateUnit(associatedUnit.GetPath(), associatedBuilding.GetComponent<ProductionBuilding>().GetSpawnPointCoords(), Quaternion.identity, associatedBuilding.botIndex);
            unit.GetComponent<MovableUnit>().Init(associatedBuilding.GetComponent<ProductionBuilding>().GetBannerCoords());
        }

        Destroy(gameObject);
            
    }
    public override void Cancel()
    {
        PlayerManager.playerManager.PayBack(associatedUnit.costs, associatedUnit.pop);
        base.Cancel();
    }

    public Sprite GetSprite()
    {
        return associatedUnit.iconSprite;
    }
}
