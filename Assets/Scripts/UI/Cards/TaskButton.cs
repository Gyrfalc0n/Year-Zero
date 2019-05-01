using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton : MonoBehaviour
{
    Task task;
    [SerializeField] Image image;

    public void Init(Task task)
    {
        this.task = task;
        if (task.GetComponent<Task>() != null && task.GetComponent<Task>().GetSprite() != null)
        {
            image.gameObject.SetActive(true);
            image.sprite = task.GetComponent<Task>().GetSprite();
        }
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
