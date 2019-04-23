using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class BuildTool : Tool
{
    ConstructedUnit associatedBuilding;

    public Sprite defaultSprite;

    public void Init(ConstructedUnit building)
    {
        associatedBuilding = building;
        SetButtonSprite(building);
        GetComponentInChildren<Text>().text = building.objName;
    }

    public void Build()
    {
        if (PlayerManager.playerManager.PayCheck(associatedBuilding.costs, associatedBuilding.pop, true))
        {
            BuilderUnit builder = SelectUnit.selectUnit.selected[SelectUnit.selectUnit.underSelected].GetComponent<BuilderUnit>();
            PlayerController.playerController.InitBuildToolControls(associatedBuilding, builder);
        }
        SelectUnit.selectUnit.UpdateUI();
    }

    public ConstructedUnit GetAssociatedBuilding()
    {
        return associatedBuilding;
    }

    void SetButtonSprite(DestructibleUnit unit)
    {
        if (unit.iconSprite)
            GetComponentInChildren<Image>().sprite = unit.iconSprite;
    }
}
