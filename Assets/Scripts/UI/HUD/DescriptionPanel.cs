using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DescriptionPanel : MonoBehaviour {

    [SerializeField]
    private GameObject panel;

    [SerializeField]
    Text objName;

    [SerializeField]
    Transform costPanel;

    [SerializeField]
    ResourceTextDescrPanel[] costs = new ResourceTextDescrPanel[3];
    [SerializeField]
    ResourceTextDescrPanel pop;

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

        for (int i = costs.Length - 1; i >= 0; i--)
        {
            costs[i].resourceName.text = PlayerManager.playerManager.GetResourcesName()[i] + " :";
            costs[i].value.text = (obj.costs[i]).ToString();
        }
        pop.gameObject.SetActive(true);
        pop.value.text = obj.GetComponent<SelectableObj>().pop.ToString();

        description.text = obj.description;
    }

    public void ResetPanel()
    {
        pop.gameObject.SetActive(false);
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
                SelectableObj building = tmp.GetAssociatedBuilding().GetComponent<SelectableObj>();
                Init(building);
                found = true;
            }
            else if (raycastResultList[i].gameObject.GetComponent<TaskTool>() != null)
            {
                TaskTool tmp = raycastResultList[i].gameObject.GetComponent<TaskTool>();
                Init(tmp.GetAssociatedUnit());
                found = true;
            }
        }
        if (!found)
        {
            ResetPanel();
        }
    }
}
