using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;

[RequireComponent(typeof(PauseControls))]
[RequireComponent(typeof(MovementControls))]
[RequireComponent(typeof(MoveToolControls))]
[RequireComponent(typeof(BuildToolControls))]
[RequireComponent(typeof(PatrolToolControls))]
[RequireComponent(typeof(AttackToolControls))]
[RequireComponent(typeof(MinimapMarkerControls))]
[RequireComponent(typeof(ChatPanelControls))]
[RequireComponent(typeof(AlliesPanelControls))]
[RequireComponent(typeof(ChatMenuPanelControls))]
[RequireComponent(typeof(RepairToolControls))]
[RequireComponent(typeof(SpellToolControls))]
[RequireComponent(typeof(SkilltreeControls))]
[RequireComponent(typeof(HelpPanelControls))]
public class PlayerController : MonoBehaviour {

    MovementControls movementControls;
    PauseControls pauseControls;
    PatrolToolControls patrolToolControls;
    BuildToolControls buildToolControls;
    AttackToolControls attackToolControls;
    MoveToolControls moveToolControls;
    MinimapMarkerControls minimapMarkerControls;
    ChatPanelControls chatPanelControls;
    AlliesPanelControls alliesPanelControls;
    ChatMenuPanelControls chatMenuPanelControls;
    RepairToolControls repairToolControls;
    SpellToolControls spellToolControls;
    SkilltreeControls skilltreeControls;
    HelpPanelControls helpPanelControls;
    PlayerControls currentPlayerControls;

    #region Singleton

    public static PlayerController playerController;

    private void Awake()
    {
        playerController = this;
        moveToolControls = GetComponent<MoveToolControls>();
        pauseControls = GetComponent<PauseControls>();
        movementControls = GetComponent<MovementControls>();
        buildToolControls = GetComponent<BuildToolControls>();
        patrolToolControls = GetComponent<PatrolToolControls>();
        attackToolControls = GetComponent<AttackToolControls>();
        minimapMarkerControls = GetComponent<MinimapMarkerControls>();
        chatPanelControls = GetComponent<ChatPanelControls>();
        alliesPanelControls = GetComponent<AlliesPanelControls>();
        chatMenuPanelControls = GetComponent<ChatMenuPanelControls>();
        repairToolControls = GetComponent<RepairToolControls>();
        spellToolControls = GetComponent<SpellToolControls>();
        skilltreeControls = GetComponent<SkilltreeControls>();
        helpPanelControls = GetComponent<HelpPanelControls>();
        currentPlayerControls = movementControls.Activate();
    }

    #endregion

    [SerializeField]
    ToolsPanel toolsPanel;
    [SerializeField]
    CardsPanel cardsPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && CameraAvailable())
        {
            InitPauseControls();
        }
        CheckCurrentPlayerControls();
    }

    void CheckCurrentPlayerControls()
    {
        if (!currentPlayerControls.isActive)
        {
            InitMovementControls();
        }
    }

    void ResetCurrentPlayerControls()
    {
        currentPlayerControls.Cancel();
    }

    public void StopSelection()
    {
        movementControls.StopSelection();
    }

    BuilderUnit GetUnderSelectedBuilder()
    {
        return SelectUnit.selectUnit.selected[SelectUnit.selectUnit.underSelected].GetComponent<BuilderUnit>();
    }

    public void ShowBuildingTools()
    {
        toolsPanel.ShowToolsList(GetUnderSelectedBuilder().buildings);
    }

    public void HideBuildingTools()
    {
        toolsPanel.ShowToolsList(GetUnderSelectedBuilder().tools);
    }

    void InitControls(PlayerControls control)
    {
        ResetCurrentPlayerControls();
        currentPlayerControls = control.Activate();
    }

    public void InitMovementControls()
    {
        InitControls(movementControls);
    }

    public void InitPauseControls()
    {
        InitControls(pauseControls);
    }

    public void InitPatrolToolControls()
    {
        InitControls(patrolToolControls);
    }

    public void InitMoveToolControls()
    {
        InitControls(moveToolControls);
    }

    public void InitBuildToolControls(ConstructedUnit building, BuilderUnit builder)
    {
        InitControls(buildToolControls);
        buildToolControls.CreatePlacementGrid(building, builder);
    }

    public void InitAttackToolControls()
    {
        InitControls(attackToolControls);
    }

    public void InitMinimapMarkerControls()
    {
        InitControls(minimapMarkerControls);
    }

    public void InitChatPanelControls()
    {
        InitControls(chatPanelControls);
    }

    public void InitAlliesPanelControls()
    {
        InitControls(alliesPanelControls);
    }

    public void InitChatMenuPanelControls()
    {
        InitControls(chatMenuPanelControls);
    }

    public void InitRepairToolControls()
    {
        InitControls(repairToolControls);
    }

    public void InitSpellToolControls(Spell spell)
    {
        InitControls(spellToolControls);
        spellToolControls.Init(spell);
    }

    public void InitSkilltreePanelControls()
    {
        InitControls(skilltreeControls);
    }

    public void InitHelpPanelControls()
    {
        InitControls(helpPanelControls);
    }

    public bool CameraAvailable()
    {
        return (!SelectUnit.selectUnit.isSelecting && 
            currentPlayerControls != pauseControls &&
            currentPlayerControls != chatPanelControls &&
            currentPlayerControls != alliesPanelControls &&
            currentPlayerControls != chatMenuPanelControls &&
            currentPlayerControls != skilltreeControls &&
            currentPlayerControls != helpPanelControls);
    }
}
