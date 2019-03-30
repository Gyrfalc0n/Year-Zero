using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotInstantiationManager : MonoBehaviour
{
    [SerializeField]
    GameObject instantiateTaskPrefab;

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
            if (m.mySelectableObjs[i] == null)
                continue;
            if (m.mySelectableObjs[i].GetComponent<ProductionBuilding>() != null && m.mySelectableObjs[i].GetComponent<ProductionBuilding>().CanProduct(unit))
                res.Add(m.mySelectableObjs[i].GetComponent<ProductionBuilding>());
        }

        return res;
    }

    public int CreateUnit(int unitIndex, out InstantiateTask task)
    {
        task = null;
        MovableUnit unit =((GameObject)Resources.Load(troopList[unitIndex])).GetComponent<MovableUnit>();
        int pay = GetComponent<BotManager>().GetPayLimiterIndex(unit.costs, unit.pop);
        if (pay != -1) return pay;
        List<ProductionBuilding> buildings = GetAvailableProductionBuilding(unit);
        foreach (ProductionBuilding building in buildings)
        {
            if (CreateInstantiateTask(building, unit, out task))
                return -1;
        }
        return -3;
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

    public bool CreateInstantiateTask(ProductionBuilding building, MovableUnit unit, out InstantiateTask task)
    {
        task = null;
        if (building.GetComponent<TaskSystem>().Full())
            return false;
        if (!GetComponent<BotManager>().Pay(unit.costs, unit.pop))
            print("wtf");
        task = Instantiate(instantiateTaskPrefab).GetComponent<InstantiateTask>();
        task.FirstInit(building);
        task.Init(unit);
        building.GetComponent<TaskSystem>().Add(task);
        SelectUnit.selectUnit.UpdateUI();
        return true;
    }

    public MovableUnit GetUnitOfIndex(int index)
    {
        return ((GameObject)Resources.Load(troopList[index])).GetComponent<MovableUnit>();
    }
}
