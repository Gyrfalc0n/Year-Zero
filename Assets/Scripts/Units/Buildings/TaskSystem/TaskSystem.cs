using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    List<Task> tasks = new List<Task>();

    void Update()
    {
        if (tasks.Count > 0)
        {
            if (tasks[0].Finished())
            {
                Remove(tasks[0]);
            }
            if (tasks.Count > 0)
                tasks[0].UpdateTask();
        }
    }

    public void Add(Task task)
    {
        tasks.Add(task);
    }

    void Remove(Task task)
    {
        tasks.Remove(task);
    }

    public void Cancel(Task task)
    {
        task.Cancel();
        Destroy(task.gameObject);
        Remove(task);
    }

    public float GetCurrentAdvancement()
    {
        float res = 0f;
        if (tasks.Count > 0)
            res = tasks[0].GetCurrentAdvancement();
        return res;
    }

    public List<Task> GetTasks()
    {
        return tasks;
    }

    public bool Empty()
    {
        return tasks.Count == 0;
    }
}
