using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildTool : Tool {

    [HideInInspector]
    public string path = "Buildings/";

    public string building;

    public void Build()
    {
        if (CheckCosts(((GameObject)Resources.Load(path + building + "Unit")).GetComponent<ConstructedUnit>().costs))
        {
            BuilderUnit builder = SelectUnit.selectUnit.selected[SelectUnit.selectUnit.underSelected].GetComponent<BuilderUnit>();
            PlayerController.playerController.InitBuildToolControls(path + building, builder);
        }
    }

    private bool CheckCosts(int[] costs)
    {
        if (PlayerManager.playerManager.GetWood() >= costs[0] &&
            PlayerManager.playerManager.GetStone() >= costs[1] &&
            PlayerManager.playerManager.GetGold() >= costs[2] &&
            PlayerManager.playerManager.GetMeat() >= costs[3])
        {
            return true;
        }
        else
        {
            Debug.Log("Not enough resources");
            return false;
        }
    }
}
