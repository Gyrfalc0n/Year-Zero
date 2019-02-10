using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTaskTool : Tool
{
    [SerializeField]
    GameObject upgradeTaskPrefab;

    ConstructedUnit currentBuilding;
    ConstructedUnit nextBuilding;

    public void Init(ConstructedUnit building, ConstructedUnit next)
    {
        currentBuilding = building;
        nextBuilding = next;
        GetComponentInChildren<Text>().text = nextBuilding.objName;
    }

    public void CreateUpgradeTask()
    {
        if (currentBuilding.GetComponent<TaskSystem>().Empty() && PlayerManager.playerManager.Pay(nextBuilding.costs, nextBuilding.pop))
        {
            UpgradeTask task = Instantiate(upgradeTaskPrefab).GetComponent<UpgradeTask>();
            task.FirstInit(currentBuilding);
            task.Init(currentBuilding, nextBuilding);
            currentBuilding.GetComponent<TaskSystem>().Add(task);
            SelectUnit.selectUnit.UpdateUI();
        }
    }

    public ConstructedUnit GetAssociatedBuilding()
    {
        return nextBuilding;
    }
}
