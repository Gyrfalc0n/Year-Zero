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
public class PlayerController : MonoBehaviour {

    MovementControls movementControls;
    PauseControls pauseControls;
    PatrolToolControls patrolToolControls;
    BuildToolControls buildToolControls;
    AttackToolControls attackToolControls;
    MoveToolControls moveToolControls;

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
        currentPlayerControls = movementControls.Activate();
    }

    #endregion

    [SerializeField]
    ToolsPanel toolsPanel;
    [SerializeField]
    CardsPanel cardsPanel;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && currentPlayerControls != pauseControls)
        {
            InitPauseControls();
        }
        CheckCurrentPlayerControls();
    }

    void CheckCurrentPlayerControls()
    {
        if (!currentPlayerControls.IsActive())
        {
            ResetCurrentPlayerControls();
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



    public void ShowBuildingTools()
    {
        BuilderUnit builder = SelectUnit.selectUnit.selected[SelectUnit.selectUnit.underSelected].GetComponent<BuilderUnit>();
        toolsPanel.ShowToolsList(builder.buildings);
    }

    public void HideBuildingTools()
    {
        BuilderUnit builder = SelectUnit.selectUnit.selected[SelectUnit.selectUnit.underSelected].GetComponent<BuilderUnit>();
        toolsPanel.ShowToolsList(builder.tools);
    }

    public void InitMovementControls()
    {
        ResetCurrentPlayerControls();
        currentPlayerControls = movementControls.Activate();
    }

    public void InitPauseControls()
    {
        ResetCurrentPlayerControls();
        currentPlayerControls = pauseControls.Activate();
        pauseControls.Init();
    }

    public void InitPatrolToolControls()
    {
        ResetCurrentPlayerControls();
        currentPlayerControls = patrolToolControls.Activate();
    }

    public void InitMoveToolControls()
    {
        ResetCurrentPlayerControls();
        currentPlayerControls = moveToolControls.Activate();
    }

    public void InitBuildToolControls(string building, BuilderUnit builder)
    {
        ResetCurrentPlayerControls();
        currentPlayerControls = buildToolControls.Activate();
        buildToolControls.CreatePlacementGrid(building, builder);
    }

    public void InitAttackToolControls()
    {
        ResetCurrentPlayerControls();
        currentPlayerControls = attackToolControls.Activate();
    }
}
