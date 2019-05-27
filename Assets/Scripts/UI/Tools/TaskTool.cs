using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTool : Tool
{
    [SerializeField]
    GameObject taskPrefab;

    ConstructedUnit associatedBuilding;
    public MovableUnit associatedUnit { get; private set; }
    [SerializeField] Image image;

    public void Init(ConstructedUnit building, MovableUnit unit)
    {
        associatedBuilding = building;
        associatedUnit = unit;
        SetButtonSprite(unit);
    }

    public void CreateInstantiateTask()
    {
        if (!associatedBuilding.GetComponent<TaskSystem>().Full() && PlayerManager.playerManager.Pay(associatedUnit.costs, associatedUnit.pop, false))
        {
            Task task = Instantiate(taskPrefab).GetComponent<Task>();
            task.transform.SetParent(associatedBuilding.GetComponent<TaskSystem>().taskHolder);
            task.Init(associatedBuilding, associatedUnit);
            associatedBuilding.GetComponent<TaskSystem>().Add(task);
            SelectUnit.selectUnit.UpdateUI();
        }
    }

    void SetButtonSprite(DestructibleUnit unit)
    {
        if (unit.iconSprite)
        {
            GetComponentInChildren<Text>().gameObject.SetActive(false);
            image.sprite = unit.iconSprite;
        }
        else
        {
            GetComponentInChildren<Text>().text = associatedUnit.objName;
            image.gameObject.SetActive(false);
        }
    }

    public void OnPointerEnter()
    {
        DescriptionPanel.m.Init(associatedUnit);
    }

    public void OnPointerExit()
    {
        DescriptionPanel.m.ResetPanel();
    }
}
