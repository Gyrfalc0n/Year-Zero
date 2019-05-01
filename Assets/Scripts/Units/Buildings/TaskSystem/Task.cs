using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    float requiredTime;
    float remainingTime;

    ConstructedUnit associatedBuilding;
    MovableUnit associatedUnit;

    bool active;

    public void Init(ConstructedUnit building, MovableUnit unit)
    {
        associatedBuilding = building;
        associatedUnit = unit;
        active = true;
        remainingTime = unit.GetRequiredTime();
        requiredTime = unit.GetRequiredTime();
    }

    public void UpdateTask()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            OnFinishedTask();
        }
    }

    void OnFinishedTask()
    {
        active = false;
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
            GameObject unit = InstanceManager.instanceManager.InstantiateUnit(associatedUnit.GetPath(), destroyer.transform.position + new Vector3(-5, 0, 0), Quaternion.identity, associatedBuilding.botIndex);
            unit.GetComponent<MovableUnit>().Init(destroyer.transform.position + new Vector3(-10, 0, 0));
        }
        else
        {
            GameObject unit = InstanceManager.instanceManager.InstantiateUnit(associatedUnit.GetPath(), associatedBuilding.GetComponent<ProductionBuilding>().GetSpawnPointCoords(), Quaternion.identity, associatedBuilding.botIndex);
            unit.GetComponent<MovableUnit>().Init(associatedBuilding.GetComponent<ProductionBuilding>().GetBannerCoords());
        }

        Destroy(gameObject);
    }

    public bool Finished()
    {
        return !active;
    }

    public void Cancel()
    {
        PlayerManager.playerManager.PayBack(associatedUnit.costs, associatedUnit.pop);
        active = false;
    }

    public float GetCurrentAdvancement()
    {
        return 1 - remainingTime / requiredTime;
    }

    public ConstructedUnit GetBuilding()
    {
        return associatedBuilding;
    }

    public Sprite GetSprite()
    {
        return associatedUnit.iconSprite;
    }
}
