using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskTool : Tool
{
    [SerializeField]
    GameObject instantiatePrefab;
    [SerializeField]
    Image img;
    [SerializeField]
    float requiredTime;

    public void CreateInstantiateTask(string name)
    {
        string path = "Units/" + name;
        GameObject unit = ((GameObject)Resources.Load(path));
        InstantiateTask task = Instantiate(instantiatePrefab).GetComponent<InstantiateTask>();
        task.Init(unit.GetComponent<MovableUnit>(), path, requiredTime);
        SelectUnit.selectUnit.selected[0].GetComponent<TaskSystem>().Add(task);
        //TaskBar.taskBar.UpdateQueue(SelectUnit.selectUnit.selected[0].GetComponent<TaskSystem>().tasks);
    }
}
