using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInstantiationManager : MonoBehaviour
{
    [SerializeField]
    GameObject instantiateTaskPrefab;
    int remaining;
    int currentUnitIndex;

    string[] troopList = new string[]
{
        "Units/Basic Troop",
        "Units/Bomber",
        "Units/Builder",
        "Units/Destroyer",
        "Units/Hacker",
        "Units/Light Troop",
        "Units/Mobile Medical Station",
};

    List<ProductionBuilding> GetAvailableProductionBuilding(MovableUnit unit)
    {
        List<ProductionBuilding> res = new List<ProductionBuilding>();

        IAManager m = GetComponent<IAManager>();
        for (int i = 0; i < m.mySelectableObjs.Count; i++)
        {
            if (m.mySelectableObjs[i].GetComponent<ProductionBuilding>() != null && m.mySelectableObjs[i].GetComponent<ProductionBuilding>().CanProduct(unit))
                res.Add(m.mySelectableObjs[i].GetComponent<ProductionBuilding>());
        }

        return res;
    }

    private void Update()
    {
        CheckProduction();
        if (Input.GetKeyDown(KeyCode.E))
        {
            AskForUnits(2, 3);
        }
    }

    void AskForUnits(int index, int number)
    {
        if (remaining != 0)
            return;

        remaining = number;
        currentUnitIndex = index;
    }

    void CheckProduction()
    {
        if (remaining > 0)
        {
            remaining = CreateUnit(currentUnitIndex, remaining);
        }
    }

    public int CreateUnit(int unitIndex, int number)
    {
        MovableUnit unit =((GameObject)Resources.Load(troopList[unitIndex])).GetComponent<MovableUnit>();
        int remaining = number;
        List<ProductionBuilding> buildings = GetAvailableProductionBuilding(unit);
        bool cannotContinue = true;
        bool stop = false;
        for (int i = 0; remaining > 0 && !stop; i++)
        {
            if (i == buildings.Count)
            {
                i = 0;
                stop = cannotContinue;
                cannotContinue = true;
            }

            if (!stop && CreateInstantiateTask(buildings[i], unit))
            {
                cannotContinue = false;
                remaining--;
            }
        }
        return remaining;
    }

    List<ProductionBuilding> GetCompatibleProductionBuildings(ProductionBuilding building, MovableUnit unit)
    {
        List<ProductionBuilding> res = new List<ProductionBuilding>();

        IAManager m = GetComponent<IAManager>();
        for (int i = 0; i < m.mySelectableObjs.Count; i++)
        {
            if (m.mySelectableObjs[i].GetComponent<ProductionBuilding>() != null && m.mySelectableObjs[i].GetComponent<ProductionBuilding>().CanProduct(unit))
            {
                res.Add(m.mySelectableObjs[i].GetComponent<ProductionBuilding>());
            }
        }
        return res;
    }

    public bool CreateInstantiateTask(ProductionBuilding building, MovableUnit unit)
    {
        if (!building.GetComponent<TaskSystem>().Full() && PlayerManager.playerManager.Pay(unit.costs, unit.pop))
        {
            InstantiateTask task = Instantiate(instantiateTaskPrefab).GetComponent<InstantiateTask>();
            task.FirstInit(building);
            task.Init(unit);
            building.GetComponent<TaskSystem>().Add(task);
            SelectUnit.selectUnit.UpdateUI();
            return true;
        }
        return false;
    }
}
