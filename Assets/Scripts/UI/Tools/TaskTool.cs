using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTool : Tool
{
    [SerializeField]
    GameObject instantiateTaskPrefab;

    public Sprite defaultSprite;

    ConstructedUnit associatedBuilding;
    MovableUnit associatedUnit;

    public void Init(ConstructedUnit building, MovableUnit unit)
    {
        associatedBuilding = building;
        associatedUnit = unit;
        SetButtonSprite(unit);
        GetComponentInChildren<Text>().text = associatedUnit.objName;
    }

    public void CreateInstantiateTask()
    {
        if (!associatedBuilding.GetComponent<TaskSystem>().Full() && PlayerManager.playerManager.Pay(associatedUnit.costs, associatedUnit.pop))
        {
            InstantiateTask task = Instantiate(instantiateTaskPrefab).GetComponent<InstantiateTask>();
            task.FirstInit(associatedBuilding);
            task.Init(associatedUnit);
            associatedBuilding.GetComponent<TaskSystem>().Add(task);
            SelectUnit.selectUnit.UpdateUI();
        }
    }

    public MovableUnit GetAssociatedUnit()
    {
        return associatedUnit;
    }

    void SetButtonSprite(DestructibleUnit unit)
    {
        if (unit.iconSprite)
            GetComponentInChildren<Image>().sprite = unit.iconSprite;
    }
}
