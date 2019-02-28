﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SelectableObj : Interactable
{

    [SerializeField]
    string path;

    public List<GameObject> tools;

    [HideInInspector]
    public bool highlighted = false;
    [HideInInspector]
    public bool selected = false;

    protected Color32 myColor = new Color32(18, 255, 0, 255);
    protected Color32 teamColor = new Color32(170, 170, 0, 255);
    protected Color32 enemyColor = new Color32(200, 60, 50, 255);

    public string objName;
    [TextArea(3, 5)]
    public string description;
    public int[] costs = new int[3];
    public int pop;

    string minimapIconPrefabPath = "Units/MinimapIconPrefab";
    SpriteRenderer minimapIcon;

    string selectionCirclePath = "Units/SelectionCircle";
    protected SpriteRenderer selectionCircle;

    string fieldOfViewPrefabPath = "VFX/FogOfWar/FieldOfViewPrefab";
    public FieldOfViewCollider fovCollider;
    bool visible;

    public virtual void Awake()
    {
        InitSelectionCircle();
        ToggleColor(1);
        InstanceManager.instanceManager.allSelectableObjs.Add(this);
        if (photonView.IsMine)
        {
            InstanceManager.instanceManager.mySelectableObjs.Add(this);
        }
        InitFieldOfView();
    }

    Transform spellHolder;
    [HideInInspector]
    public List<GameObject> spells = new List<GameObject>();

    public virtual void Start()
    {
        spellHolder = transform.Find("Spell Holder");
        foreach (GameObject obj in tools)
        {
            if (obj.GetComponent<Spell>() != null)
            {
                GameObject tmp = Instantiate(obj, spellHolder);
                tmp.GetComponent<Spell>().associatedUnit = this;
                spells.Add(tmp);
            }
        }
    }

    public void InitSelectionCircle()
    {
        selectionCircle = ((GameObject)Instantiate(Resources.Load(selectionCirclePath), transform)).GetComponent<SpriteRenderer>();
        selectionCircle.transform.localPosition = GetSelectionCirclePos();
        selectionCircle.transform.localScale = new Vector3(1, 1, 1);
        selectionCircle.gameObject.SetActive(false);

        minimapIcon = ((GameObject)Instantiate(Resources.Load(minimapIconPrefabPath), transform)).GetComponent<SpriteRenderer>();
        minimapIcon.transform.localPosition = new Vector3(0, 5, 0);

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
            minimapIcon.color = enemyColor;
        }

    }

    public void ToggleColor(int advancedLvl)
    {
        if (advancedLvl == 0)
        {
            if ((int)photonView.Owner.CustomProperties["Team"] == InstanceManager.instanceManager.GetTeam())
            {
                minimapIcon.color = myColor;
            }
        }
        else if (advancedLvl == 1)
        {
            if (photonView.IsMine)
            {
                minimapIcon.color = InstanceManager.instanceManager.GetColor();
            }
            else if ((int)photonView.Owner.CustomProperties["Team"] == InstanceManager.instanceManager.GetTeam())
            {
                minimapIcon.color = teamColor;
            }
        }
        else
        {
            if ((int)photonView.Owner.CustomProperties["Team"] == InstanceManager.instanceManager.GetTeam())
            {
                minimapIcon.color = InstanceManager.instanceManager.GetPlayerColor(photonView.Owner);
            }
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
        fovCollider = ((GameObject)Instantiate(Resources.Load(fieldOfViewPrefabPath), transform)).GetComponent<FieldOfViewCollider>();
        fovCollider.transform.localPosition = new Vector3(0, 0.51f, 0);
        if (GetComponent<MovableUnit>() != null)
        {
            fovCollider.transform.localScale = new Vector3(2, 1, 2);
        }
        else if (GetComponent<Radar>() != null)
        {
            fovCollider.transform.localScale = new Vector3(4, 1, 4);
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
        minimapIcon.gameObject.SetActive(false);
        Dehighlight();
        Deselect();
    }

    public void UnHide()
    {
        visible = true;
        minimapIcon.gameObject.SetActive(true);
        GetComponent<MeshRenderer>().enabled = true;
    }

    #endregion
}
