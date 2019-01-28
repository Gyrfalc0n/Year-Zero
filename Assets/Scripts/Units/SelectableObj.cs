using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SelectableObj : Interactable {

    [SerializeField]
    string path;

    [SerializeField]
    float requiredTime;

    public List<GameObject> tools;

    [HideInInspector]
    public bool highlighted = false;
    [HideInInspector]
    public bool selected = false;

    protected Color32 myColor = new Color32(18, 255, 0, 255);
    protected Color32 teamColor = new Color32(170, 170, 0, 255);
    protected Color32 enemyColor = new Color32(200, 60, 50, 255);

    public string objName;
    [TextArea(3,5)]
    public string description;
    public int[] costs = new int[3];
    public int pop;

    string selectionCirclePath = "Units/SelectionCircle";
    SpriteRenderer selectionCircle;

    [SerializeField]
    FieldOfViewCollider fieldOfViewPrefab;
    FieldOfViewCollider fovCollider;
    bool visible;

    public virtual void Awake()
    {
        InitSelectionCircle();
        InstanceManager.instanceManager.allSelectableObjs.Add(this);
        if (photonView.IsMine)
        {
            InstanceManager.instanceManager.mySelectableObjs.Add(this);
        }
        InitFieldOfView();
    }

    public void InitSelectionCircle()
    {
        selectionCircle = ((GameObject)Instantiate(Resources.Load(selectionCirclePath), transform)).GetComponent<SpriteRenderer>();
        selectionCircle.transform.localPosition = GetSelectionCirclePos();
        selectionCircle.transform.localScale = new Vector3(1, 1, 1);
        selectionCircle.gameObject.SetActive(false);

        if (photonView.IsMine)
        {
            selectionCircle.color = myColor;
        }
        else if ((int)photonView.Owner.CustomProperties["Team"] == InstanceManager.instanceManager.GetTeam())
        {
            selectionCircle.color = teamColor;
        }
        else
        {
            selectionCircle.color = enemyColor;
        }
        
    }

    public virtual Vector3 GetSelectionCirclePos()
    {
        return Vector3.zero;
    }

    public virtual void Select()
    {
        if (!visible)
            return;
        if (highlighted)
            Dehighlight();
        selected = true;
        selectionCircle.gameObject.SetActive(true);
        selectionCircle.color = myColor;
    }

    public virtual void Deselect()
    {
        selected = false;
        selectionCircle.gameObject.SetActive(false);
    }

    public void Highlight()
    {
        if (!visible)
            return;
        highlighted = true;
        if (!selected)
        {
            highlighted = true;
            selectionCircle.gameObject.SetActive(true);
            Color32 tmp = (Color32)selectionCircle.color;
            selectionCircle.color = new Color32(tmp.r, tmp.g, tmp.b, 50);
        }

    }

    public void Dehighlight()
    {
        highlighted = false;
        if (!selected)
            selectionCircle.gameObject.SetActive(false);
    }

    public virtual void Interact(Interactable obj) { }

    public float GetRequiredTime()
    {
        return requiredTime;
    }

    public string GetPath()
    {
        return path;
    }

    #region FOV

    void InitFieldOfView()
    {
        if (PhotonNetwork.OfflineMode)
        {
            visible = true;
        }
        else
        {
            visible = (int)photonView.Owner.CustomProperties["Team"] == InstanceManager.instanceManager.GetTeam();
        }
        if (visible)
        {
            UnHide();
        }
        else
        {
            Hide();
            return;
        }
        fovCollider = Instantiate(fieldOfViewPrefab, transform);
        fovCollider.transform.localPosition = new Vector3(0, 0.51f, 0);
        if (GetComponent<MovableUnit>() != null)
        {
            fovCollider.transform.localScale = new Vector3(2, 1, 2);
        }
        else if (GetComponent<ConstructedUnit>() != null || GetComponent<InConstructionUnit>() != null)
        {
            fovCollider.transform.localScale = new Vector3(3, 1, 3);
        }
    }

    public void Hide()
    {
        visible = false;
        GetComponent<MeshRenderer>().enabled = false;
        Dehighlight();
        Deselect();
    }

    public void UnHide()
    {
        visible = true;
        GetComponent<MeshRenderer>().enabled = true;
    }

    #endregion
}
