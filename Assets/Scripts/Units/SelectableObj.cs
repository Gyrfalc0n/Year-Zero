using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SelectableObj : Interactable {

    [SerializeField]
    string path;

    [SerializeField]
    float requiredTime;

    public List<Tool> tools;

    [HideInInspector]
    public bool highlighted = false;
    [HideInInspector]
    public bool selected = false;

    protected Color32 myColor = new Color32(18, 255, 0, 255);
    protected Color32 myHighlightColor = new Color32(18, 255, 0, 50);
    protected Color32 othersColor = new Color32(91, 238, 161, 255);

    public string objName;
    [TextArea(3,5)]
    public string description;
    public int[] costs = new int[5];

    SpriteRenderer selectionCircle;

    public virtual void Awake()
    {
        selectionCircle = transform.Find("SelectionCircle").GetComponent<SpriteRenderer>();
        selectionCircle.color = myColor;
        InstanceManager.instanceManager.allSelectableObjs.Add(this);
        if (photonView.IsMine)
        {
            InstanceManager.instanceManager.mySelectableObjs.Add(this);
        }
    }

    public void Select()
    {
        if (highlighted)
            Dehighlight();
        selected = true;
        selectionCircle.gameObject.SetActive(true);
        selectionCircle.color = myColor;
    }

    public void Deselect()
    {
        selected = false;
        selectionCircle.gameObject.SetActive(false);
    }

    public void Highlight()
    {
        if (selected)
            return;
        highlighted = true;
        selectionCircle.gameObject.SetActive(true);
        selectionCircle.color = myHighlightColor;
    }

    public void Dehighlight()
    {
        highlighted = false;
        selectionCircle.gameObject.SetActive(false);
    }

    public virtual void Interact(Interactable obj) { }

    public string GetPath()
    {
        return path;
    }

    public float GetRequiredTime()
    {
        return requiredTime;
    }
}
