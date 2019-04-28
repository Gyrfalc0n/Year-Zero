using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class BuildTool : Tool
{
    ConstructedUnit associatedBuilding;
    [SerializeField] Image image;

    public void Init(ConstructedUnit building)
    {
        associatedBuilding = building;
        SetButtonSprite(building);
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
        {
            GetComponentInChildren<Text>().gameObject.SetActive(false);
            image.sprite = unit.iconSprite;
        }
        else
        {
            GetComponentInChildren<Text>().text = associatedBuilding.objName;
            image.gameObject.SetActive(false);
        }
    }
}
