using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInstantiationManager : MonoBehaviour
{
    [SerializeField]
    GameObject instantiateTaskPrefab;

    string[] unitList = new string[]
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

    public int CreateUnit(int unitIndex, int number)
    {
        MovableUnit unit =((GameObject)Resources.Load(unitList[unitIndex])).GetComponent<MovableUnit>();
        int remaining = number;
        List<ProductionBuilding> buildings = GetAvailableProductionBuilding(unit);

        bool allFull = false;
        for (int i = 0; remaining > 0 && !allFull; i++)
        {
            if (i == buildings.Count)
                i = 0;

            allFull = true;
            if (CreateInstantiateTask(buildings[i], unit))
            {
                allFull = false;
                remaining--;
            }
        }

        return remaining;
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
        if (!PlayerManager.playerManager.PayCheck(unit.costs, unit.pop))
            Debug.LogError("Problem");
        return false;
    }
}
