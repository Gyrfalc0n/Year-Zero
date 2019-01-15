using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolsPanel : MonoBehaviour {

    [SerializeField]
    private Transform buttons;

    private void ClearTools()
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

    public void ShowToolsList(List<Tool> list)
    {
        ClearTools();
        foreach (Tool tool in list)
        {
            Instantiate(tool, buttons);
        }
    }
}
