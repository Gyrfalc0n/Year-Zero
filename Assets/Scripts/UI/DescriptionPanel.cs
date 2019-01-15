using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DescriptionPanel : MonoBehaviour {

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    private Text objName;

    [SerializeField]
    private Text[] costs = new Text[5];

    [SerializeField]
    private Text description;

    private void Update()
    {
        MouseOverTool();
    }

    public void Init(SelectableObj obj)
    {
        panel.SetActive(true);
        objName.text = obj.objName;

        for (int i = 0; i < costs.Length; i++)
        {
            costs[i].text = (obj.costs[i]).ToString();
        }

        description.text = obj.description;
    }

    public void ResetPanel()
    {
        panel.SetActive(false);
    }

    public void MouseOverTool()
    {
        bool found = false;
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for (int i = raycastResultList.Count - 1; i >= 0; i--)
        {
            if (raycastResultList[i].gameObject.GetComponent<BuildTool>() != null)
            {
                BuildTool tmp = raycastResultList[i].gameObject.GetComponent<BuildTool>();
                SelectableObj building = ((GameObject)Resources.Load(tmp.path + tmp.building + "Unit")).GetComponent<SelectableObj>();
                Init(building);
                found = true;
            }
        }
        if (!found)
        {
            ResetPanel();
        }
    }
}
