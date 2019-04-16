using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class SelectableObj : Interactable
{
    public List<GameObject> tools;
    public int team { get; private set; }

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

    string minimapIconPrefabPath = "Units/Others/MinimapIconPrefab";
    SpriteRenderer minimapIcon;

    string selectionCirclePath = "Units/Others/SelectionCircle";
    protected SpriteRenderer selectionCircle;

    protected string fieldOfViewPrefabPath = "VFX/FogOfWar/FieldOfViewPrefab";
    [HideInInspector]
    public FieldOfViewCollider fovCollider;
    bool visible;

    public int botIndex;

    public virtual void InitUnit(int botIndex)
    {
        if (!PhotonNetwork.OfflineMode)
            photonView.RPC("RPCInitUnit", RpcTarget.Others, botIndex);
        SetBotIndex(botIndex);
        team = MultiplayerTools.GetTeamOf(this);
        InitSelectionCircle();
        ToggleColor(1);
        InstanceManager.instanceManager.allSelectableObjs.Add(this);
        if (photonView.IsMine)
        {
            if (botIndex == -1)
            {
                InstanceManager.instanceManager.mySelectableObjs.Add(this);
            }
            else if (botIndex == -2)
            {
                GetComponentInParent<IndependantIAManager>().mySelectableObjs.Add(this);
            }
            else
            {
                InstanceManager.instanceManager.GetBot(botIndex).mySelectableObjs.Add(this);
            }
        }
        InitFieldOfView();
        Init2();
    }

    [PunRPC]
    public virtual void RPCInitUnit(int botIndex)
    {
        SetBotIndex(botIndex);
        team = MultiplayerTools.GetTeamOf(this);
        InitSelectionCircle();
        ToggleColor(1);
        InstanceManager.instanceManager.allSelectableObjs.Add(this);
        InitFieldOfView();
    }

    public void SetBotIndex(int index)
    {
        botIndex = index;
    }

    [PunRPC]
    public void RPCSetBotIndex(int index)
    {
        botIndex = index;
    }

    Transform spellHolder;
    [HideInInspector]
    public List<GameObject> spells = new List<GameObject>();

    public virtual void Init2()
    {
        SetHolder();
        InitSpellHolder();
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

    void InitSpellHolder()
    {
        spellHolder = new GameObject().transform;
        spellHolder.SetParent(transform);
        spellHolder.gameObject.name = "SpellHolder";
    }

    void SetHolder()
    {
        string tmp = (GetComponent<MovableUnit>() != null) ? "Movable" : "Buildings";
        string parentName = "Holder";
        if (PhotonNetwork.OfflineMode)
            parentName += (botIndex == -1) ? "Player" : botIndex.ToString();
        else
            parentName += (botIndex == -1) ? photonView.Owner.NickName : botIndex.ToString();
        GameObject newParent = GameObject.Find(parentName);
        if (newParent == null)
        {
            newParent = Instantiate((GameObject)Resources.Load("IA/HolderPrefab"));
            newParent.name = parentName;
            newParent.GetComponent<Holder>().team = MultiplayerTools.GetTeamOf(this);
        }
        transform.SetParent(newParent.transform.GetChild(0).Find(tmp));
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
            if (botIndex == -1)
            {
                selectionCircle.color = myColor;
            }
            else
            {
                int tmpteam = team;        
                if (tmpteam == InstanceManager.instanceManager.GetTeam())
                {
                    selectionCircle.color = teamColor;
                }
                else
                {
                    selectionCircle.color = enemyColor;
                    minimapIcon.color = enemyColor;
                }
            }

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
        int tmpteam = team;
        Color32 tmpColor;
        if (team != -2)
            tmpColor = (botIndex == -1) ? InstanceManager.instanceManager.GetColor() : InstanceManager.instanceManager.GetBot(botIndex).GetColor();
        else
            tmpColor = Color.blue;

        if (advancedLvl == 0)
        {
            if ((int)photonView.Owner.CustomProperties["Team"] == tmpteam)
            {
                minimapIcon.color = myColor;
            }
        }
        else if (advancedLvl == 1)
        {
            if (photonView.IsMine)
            {
                minimapIcon.color = tmpColor;
            }
            else if ((int)photonView.Owner.CustomProperties["Team"] == tmpteam)
            {
                minimapIcon.color = teamColor;
            }
        }
        else
        {
            if ((int)photonView.Owner.CustomProperties["Team"] == tmpteam)
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

    [HideInInspector]
    public bool groupHighlight = false;

    public void Highlight(bool group)
    {
        if (!visible)
            return;
        highlighted = true;
        groupHighlight = group;
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

    public virtual string GetPath()
    {
        return "none";
    }

    #region FOV

    void InitFieldOfView()
    {
        if (PhotonNetwork.OfflineMode)
        {
            visible = true;
            if (botIndex != -1)
            {
                if (botIndex != -2)
                    visible = InstanceManager.instanceManager.GetTeam() == InstanceManager.instanceManager.GetBot(botIndex).GetTeam();
                else visible = false;
            }
                
        }
        else
        {
            if (botIndex == -1)
            {
                visible = (int)photonView.Owner.CustomProperties["Team"] == InstanceManager.instanceManager.GetTeam();
            }
            else
                visible = (int)photonView.Owner.CustomProperties["Team"] == InstanceManager.instanceManager.GetBot(botIndex).GetTeam();
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

        if (visible)
        {
            UnHide();
        }
        else
        {
            Hide();
            fovCollider.GetComponent<MeshRenderer>().enabled = false;
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
