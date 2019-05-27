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
    protected bool visible;

    [HideInInspector]
    public int botIndex;

    protected AudioManager audioManager;

    public virtual void InitUnit(int botIndex)
    {
        if (!PhotonNetwork.OfflineMode)
            photonView.RPC("RPCInitUnit", RpcTarget.Others, botIndex);
        SetBotIndex(botIndex);

        if (botIndex == -1 && photonView.IsMine)
        {
            audioManager = FindObjectOfType<AudioManager>();
            audioManager.PlaySound("UnitSpawn");
        }
        
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

    protected Transform spellHolder;
    [HideInInspector]
    public List<GameObject> spells = new List<GameObject>();

    public virtual void Init2()
    {
        SetHolder();
        InitSpellHolder();
        foreach (GameObject obj in tools)
        {
            if (obj == null)
            {
                Debug.LogWarning("A tool is missing in the " + objName);
                continue;
            }

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
        selectionCircle.transform.localScale = GetSelectionCircleSize();
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
                if (!InstanceManager.instanceManager.IsEnemy(this))
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
        else if (!InstanceManager.instanceManager.IsEnemy(this))
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
            if (!InstanceManager.instanceManager.IsEnemy(this))
            {
                minimapIcon.color = myColor;
            }
        }
        else if (advancedLvl == 1)
        {
            if (photonView.IsMine)
            {
                minimapIcon.color = MultiplayerTools.GetColorOf(this);
            }
            else if (!InstanceManager.instanceManager.IsEnemy(this))
            {
                minimapIcon.color = teamColor;
            }
        }
        else
        {
            if (!InstanceManager.instanceManager.IsEnemy(this))
            {
                minimapIcon.color = MultiplayerTools.GetColorOf(this);
            }
        }


    }

    public virtual Vector3 GetSelectionCirclePos()
    {
        return Vector3.zero;
    }

    public virtual Vector3 GetSelectionCircleSize()
    {
        return new Vector3(1, 1, 1);
    }

    public virtual void Select()
    {
        if (!visible)
            return;
        if (highlighted)
            Dehighlight();
        selected = true;
        selectionCircle.gameObject.SetActive(true);
        Color32 tmp = (Color32)selectionCircle.color;
        selectionCircle.color = new Color32(tmp.r, tmp.g, tmp.b, 255);
    }

    public virtual void Deselect()
    {
        selected = false;
        selectionCircle.gameObject.SetActive(false);
    }

    [HideInInspector]
    public bool groupHighlight = false;

    public virtual void Highlight(bool group)
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

    public virtual void Dehighlight()
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

    GameObject model;
    void InitFieldOfView()
    {
        model = transform.Find("model").gameObject;
        visible = (InstanceManager.instanceManager.GetTeam() == MultiplayerTools.GetTeamOf(this));

        fovCollider = ((GameObject)Instantiate(Resources.Load(fieldOfViewPrefabPath), transform)).GetComponent<FieldOfViewCollider>();
        fovCollider.transform.localPosition = new Vector3(0, 0.51f, 0);
        Vector3 vec = Vector3.zero;
        if (GetComponent<MovableUnit>() != null)
        {
            float combat = GetComponent<CombatSystem>().range * 3;
            vec = new Vector3(combat, 1, combat);
        }
        else if (GetComponent<Radar>() != null)
        {
            vec = new Vector3(4, 1, 4);
        }
        else if (GetComponent<ConstructedUnit>() != null || GetComponent<InConstructionUnit>() != null)
        {
            vec = new Vector3(3, 1, 3);
        }
        else
            print("wt");
        fovCollider.Init(vec);

        if (visible)
        {
            UnHide();
        }
        else
        {
            fovCollider.enabled = false;
            fovCollider.GetComponent<MeshRenderer>().enabled = false;
            Hide();
        }

    }

    public void Hide()
    {
        visible = false;
        model.SetActive(false);
        minimapIcon.gameObject.SetActive(false);
        Dehighlight();
        Deselect();
    }

    public void UnHide()
    {
        visible = true;
        minimapIcon.gameObject.SetActive(true);
        model.SetActive(true);
    }

    #endregion
}
