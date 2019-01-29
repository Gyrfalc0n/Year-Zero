﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskSystem : MonoBehaviour
{
    readonly int maxTasks = 16;
    TaskBar tasksBar;

    List<Task> tasks = new List<Task>();

    void Awake()
    {
        tasksBar = GameObject.Find("TasksBar").GetComponent<TaskBar>();
    }

    void Update()
    {
        if (tasks.Count > 0)
        {
            if (tasks[0].Finished())
            {
                Remove(tasks[0]);
            }
            if (tasks.Count > 0)
            {
                tasks[0].UpdateTask();
            }
            else
            {
                SelectUnit.selectUnit.UpdateUI();
            }
                
        }
    }

    public void Add(Task task)
    {
        tasks.Add(task);
        tasksBar.UpdateQueue(tasks);
    }

    void Remove(Task task)
    {
        tasks.Remove(task);
        tasksBar.UpdateQueue(tasks);
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

    public bool Full()
    {
        return tasks.Count >= maxTasks;
    }
}
