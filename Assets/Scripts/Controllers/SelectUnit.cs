using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;

public class SelectUnit : MonoBehaviourPunCallbacks {

    #region Singleton

    public static SelectUnit selectUnit;

    private void Awake()
    {
        selectUnit = this;
    }

    #endregion

    [SerializeField]
    LayerMask interactableLayer;

    public List<SelectableObj> selected;
    public int underSelected = 0;

    Vector3 mousePos1;
    Vector3 mousePos2;

    [SerializeField]
    ToolsPanel toolsPanel;
    [SerializeField]
    CardsPanel cardsPanel;
    [SerializeField]
    AdvancementBar advancementBar;
    [SerializeField]
    TaskBar taskBar;
    [SerializeField]
    PortraitPanel portraitPanel;
    [SerializeField]
    MonoDescriptionPanel monoDescriptionPanel;

    SelectionBox selectionBox;

    [HideInInspector]
    public bool isSelecting;

    void Start()
    {
        selectionBox = GetComponent<SelectionBox>();
    }

    public void UpdateSelection()
    {
        selectionBox.CheckBox();
        CheckHighlight();
        CheckSelect();
    }

    void CheckSelect()
    {
        if (!MouseOverUI() && Input.GetMouseButtonDown(0))
        {
            isSelecting = true;
            mousePos1 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        }

        if (isSelecting && Input.GetMouseButton(0))
        {
            mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);

            if (mousePos2 != mousePos1)
                HighlightObjects();
        }

        if (isSelecting && Input.GetMouseButtonUp(0))
        {
            CalculateSelection();
            isSelecting = false;
        }
    }

    public void CalculateSelection()
    {
        bool changement = false;
        mousePos2 = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (mousePos2 == mousePos1)
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, interactableLayer.value))
            {
                if (hit.collider.GetComponent<SelectableObj>() != null && hit.collider.GetComponent<SelectableObj>().photonView.IsMine)
                {
                    if (!Input.GetKey("left ctrl"))
                        ClearSelection();
                    changement = SelectObject(hit.collider.GetComponent<SelectableObj>());
                }
            }
        }
        else
            changement = SelectObjects();

        if (changement)
        {
            UpdateUI(); 
        }
    }

    public void UpdateUI()
    {
        if (selected.Count == 0)
        {
            advancementBar.Reset();
            taskBar.Reset();
            cardsPanel.ClearCards();
            monoDescriptionPanel.Reset();
            toolsPanel.ClearTools();
            return;
        }

        if (selected[0].GetComponent<DestructibleUnit>() != null)
            portraitPanel.Init(selected[0].GetComponent<DestructibleUnit>());
        if (selected[0].GetComponent<MovableUnit>() != null)
        {
            advancementBar.Reset();
            taskBar.Reset();
            cardsPanel.ClearCards();
            monoDescriptionPanel.Reset();
            if (selected.Count > 1)
                cardsPanel.CheckCards();
            else
            {
                monoDescriptionPanel.Init(selected[0].GetComponent<DestructibleUnit>());
                toolsPanel.CheckTools(0);
            }
        }
        else
        {
            monoDescriptionPanel.Reset();
            cardsPanel.ClearCards();
            toolsPanel.ClearTools();
            if (selected[0].GetComponent<InConstructionUnit>() != null)
            {
                advancementBar.Init(selected[0].GetComponent<InConstructionUnit>());
            }
            else if (selected[0].GetComponent<ConstructedUnit>() != null)
            {
                monoDescriptionPanel.Reset();
                toolsPanel.CheckTools(0);
                if (selected[0].GetComponent<TaskSystem>().GetTasks().Count > 0)
                    taskBar.Init(selected[0].GetComponent<ConstructedUnit>());
                else
                    monoDescriptionPanel.Init(selected[0].GetComponent<DestructibleUnit>());
            }
        }
    }

    bool SelectObjects()
    {
        bool changement = false;
        Rect selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        bool metSelectableObj = false;
        List<SelectableObj> tmp = new List<SelectableObj>();
        foreach (SelectableObj selectableObj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (selectRect.Contains(Camera.main.WorldToViewportPoint(selectableObj.transform.position), true))
            {
                tmp.Add(selectableObj);
                metSelectableObj = true;
            }
        }

        if (!metSelectableObj)
            return false;

        if (!Input.GetKey("left ctrl") && metSelectableObj)
            ClearSelection();


        bool mine = false;
        foreach (SelectableObj obj in tmp)
        {
            if (obj.photonView.IsMine)
            {
                mine = true;
            }
        }
        if (mine)
        {
            for (int i = tmp.Count - 1; i >= 0; i--)
            {
                if (!tmp[i].photonView.IsMine)
                {
                    tmp.RemoveAt(i);
                }
            }
        }

        bool movable = false;
        foreach (SelectableObj obj in tmp)
        {
            if (obj.GetComponent<MovableUnit>() != null)
            {
                movable = true;
            }
        }

        if (movable)
        {
            for (int i = tmp.Count - 1; i >= 0; i--)
            {
                if (tmp[i].GetComponent<MovableUnit>() != null)
                {
                    if (SelectObject(tmp[i]))
                        changement = true;
                }
            }
        }
        else
        {
            if (SelectObject(tmp[0]))
                changement = true;
        }
        return changement;
    }

    public bool SelectObject(SelectableObj hitObj)
    {
        bool already = false;
        if (!selected.Contains(hitObj))
        {
            selected.Add(hitObj);
            hitObj.Select();
        }
        else
        {
            already = true;
        }
        return !already;
    }

    public void ClearSelection()
    {
        if (selected.Count == 0)
            return;

        foreach (SelectableObj obj in selected)
        {
            obj.Deselect();
        }
        selected.Clear();
    }

    void CheckHighlight()
    {
        RaycastHit hit;
        foreach (SelectableObj selectableObj in InstanceManager.instanceManager.allSelectableObjs)
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, interactableLayer.value))
            {
                if (hit.collider.GetComponent<SelectableObj>() == selectableObj)
                {
                    selectableObj.Highlight();
                }
            }
            else if (selectableObj.highlighted)
            {
                selectableObj.Dehighlight();
            }
        }
    }

    public void ClearHighlight()
    {
        foreach (SelectableObj selectableObj in InstanceManager.instanceManager.mySelectableObjs)
        {
            if (selectableObj.highlighted)
                selectableObj.Dehighlight();
        }
    }


    void HighlightObjects()
    {
        List<SelectableObj> tmp = new List<SelectableObj>();
        Rect selectRect = new Rect(mousePos1.x, mousePos1.y, mousePos2.x - mousePos1.x, mousePos2.y - mousePos1.y);

        foreach (SelectableObj selectableObj in InstanceManager.instanceManager.allSelectableObjs)
        {
            if (selectRect.Contains(Camera.main.WorldToViewportPoint(selectableObj.transform.position), true))
            {
                tmp.Add(selectableObj);
            }
        }

        foreach (SelectableObj obj in tmp)
        {
            obj.Highlight();
        }
    }

    public void ChangeUnderSelected(int newVal)
    {
        underSelected = newVal;
        toolsPanel.CheckTools(underSelected);
        portraitPanel.Init(selected[underSelected].GetComponent<DestructibleUnit>());
    }

    public bool InstantSelect()
    {
        if (selected.Count == 0 || selected[underSelected].GetComponent<BuilderUnit>() == null)
            return false;

        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, interactableLayer.value))
        {
            foreach (SelectableObj obj in selected)
            {
                obj.Interact(hit.collider.GetComponent<Interactable>());
            }
            return true;
        }
        return false;
    }

    public bool MouseOverUI()
    {
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current);
        pointerEventData.position = Input.mousePosition;

        List<RaycastResult> raycastResultList = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, raycastResultList);
        for (int i = raycastResultList.Count - 1; i >= 0; i--)
        {
            if (raycastResultList[i].gameObject.GetComponent<MouseThrough>() != null)
            {
                raycastResultList.RemoveAt(i);
            }
        }
        return raycastResultList.Count > 0;
    }
}
    