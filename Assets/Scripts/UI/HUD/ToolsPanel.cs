using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsPanel : MonoBehaviour {

    [SerializeField]
    Transform buttons;

    [SerializeField]
    TaskTool taskToolPrefab;
    [SerializeField]
    BuildTool buildToolPrefab;

    public void ClearTools()
    {
        while (buttons.childCount > 0)
        {
            Transform child = buttons.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    public void CheckTools(int x)
    {
        if (SelectUnit.selectUnit.selected.Count > x)
        {
            ShowToolsList(SelectUnit.selectUnit.selected[x].GetComponent<SelectableObj>().tools);
        }
    }

    public void ShowToolsList(List<GameObject> list)
    {
        ClearTools();
        foreach (GameObject tool in list)
        {
            if (tool.GetComponent<MovableUnit>() != null)
            {
                TaskTool obj = Instantiate(taskToolPrefab, buttons);
                obj.Init(SelectUnit.selectUnit.selected[0].GetComponent<ConstructedUnit>(), tool.GetComponent<MovableUnit>());
            }
            else if (tool.GetComponent<ConstructedUnit>() != null)
            {
                BuildTool obj = Instantiate(buildToolPrefab, buttons);
                obj.Init(tool.GetComponent<ConstructedUnit>());
            }
            else if (tool.GetComponent<Tool>() != null)
            {
                Instantiate(tool, buttons);
            }
            else
            {
                Debug.LogError("Wrong gameobject in tools");
            }
        }
    }
}
