using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTool : Tool
{
    [SerializeField]
    GameObject instantiateTaskPrefab;

    ConstructedUnit associatedBuilding;
    MovableUnit associatedUnit;

    public void Init(ConstructedUnit building, MovableUnit unit)
    {
        associatedBuilding = building;
        associatedUnit = unit;
        GetComponentInChildren<Text>().text = associatedUnit.objName;
    }

    public void CreateInstantiateTask()
    {
        if (PlayerManager.playerManager.Pay(associatedUnit.costs, associatedUnit.pop))
        {
            InstantiateTask task = Instantiate(instantiateTaskPrefab).GetComponent<InstantiateTask>();
            task.FirstInit(associatedBuilding);
            task.Init(associatedUnit);
            associatedBuilding.GetComponent<TaskSystem>().Add(task);
        }
    }

    public MovableUnit GetAssociatedUnit()
    {
        return associatedUnit;
    }
}
