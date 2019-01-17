using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskButton : MonoBehaviour
{
    Task task;

    public void Init(Task task)
    {
        this.task = task;
    }

    public void OnClicked()
    {
        GetComponentInParent<TaskBar>().Cancel();
    }

    public Task GetTask()
    {
        return task;
    }
}
