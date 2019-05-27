using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskButton : MonoBehaviour
{
    public Task task { get; private set; }
    [SerializeField] Image image;

    public void Init(Task task)
    {
        if (task == null) return;
        this.task = task;
        if (task.GetComponent<Task>() != null && task.GetComponent<Task>().associatedUnit.iconSprite != null)
        {
            image.gameObject.SetActive(true);
            image.sprite = task.GetComponent<Task>().associatedUnit.iconSprite;
        }
    }

    public void OnClicked()
    {
        GetComponentInParent<TaskBar>().Cancel();
    }
}
