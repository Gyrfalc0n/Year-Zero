using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTool : Tool
{
    [SerializeField]
    GameObject instantiateTaskPrefab;

    ConstructedUnit associatedBuilding;
    [SerializeField]
    MovableUnit associatedUnit;

    public void Init(ConstructedUnit building)
    {
        associatedBuilding = building;
    }

    public void CreateInstantiateTask()
    {
        InstantiateTask task = Instantiate(instantiateTaskPrefab).GetComponent<InstantiateTask>();
        task.FirstInit(associatedBuilding);
        task.Init(associatedUnit);
        associatedBuilding.GetComponent<TaskSystem>().Add(task);
        //tasksBar.UpdateQueue(associatedBuilding.GetComponent<TaskSystem>().GetTasks());
    }
}
