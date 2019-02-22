using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tool : MonoBehaviour {

	public void MoveTool()
    {
        PlayerController.playerController.InitMoveToolControls();
    }

    public void StopTool()
    {
        PlayerController.playerController.StopSelection();
    }

    public void RepairTool()
    {
       PlayerController.playerController.InitRepairToolControls();
    }

    public void ShowBuildings()
    {
        PlayerController.playerController.ShowBuildingTools();
    }

    public void HideBuildings()
    {
        PlayerController.playerController.HideBuildingTools();
    }

    public void PatrolTool()
    {
        PlayerController.playerController.InitPatrolToolControls();
    }

    public void AttackTool()
    {
        PlayerController.playerController.InitAttackToolControls();
    }
}
