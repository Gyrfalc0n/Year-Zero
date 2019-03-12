﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JoblessConstructorsPanel : MonoBehaviour
{
    [SerializeField]
    GameObject button;
    int clickCounter;

    [SerializeField]
    CameraController cam;

    List<BuilderUnit> builders = new List<BuilderUnit>();

    void Start()
    {
        button.SetActive(false);
        UpdatePanel();
    }

    public void UpdatePanel()
    {
        builders.Clear();
        bool oneBuilder = false;
        foreach (SelectableObj obj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (obj.GetComponent<BuilderUnit>() != null && obj.GetComponent<BuilderUnit>().IsDoingNothing())
            {
                oneBuilder = true;
                builders.Add(obj.GetComponent<BuilderUnit>());
            }
        }
        button.SetActive(oneBuilder);
    }

    public void OnClicked()
    {
        clickCounter = (clickCounter+1 >= builders.Count) ? 0 : ++clickCounter;
        SelectUnit.selectUnit.ClearSelection();
        SelectUnit.selectUnit.SelectObject(builders[clickCounter]);
        SelectUnit.selectUnit.UpdateUI();
        cam.LookTo(builders[clickCounter].transform.position);
    }
}