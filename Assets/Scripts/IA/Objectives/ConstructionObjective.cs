using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionObjective : IAObjective
{
    public BuildingUnits buildingUnits = BuildingUnits.None;
    [HideInInspector]
    public BuilderUnit builder;
    [HideInInspector]
    public int buildingIndex;

    InConstructionUnit inConstructionUnit;

    private void Start()
    {
        if (buildingUnits != BuildingUnits.None)
            buildingIndex = (int)buildingUnits;
    }

    public void Init(int buildingIndex)
    {
        state = ObjectiveState.Deactivated;
        this.buildingIndex = buildingIndex;
    }

    public void SetBuilder()
    {
        state = GetComponentInParent<BotBuilderManager>().GetOneBuilder(out builder, buildingIndex == 4, false);
    }

    void Update()
    {
        if (state == ObjectiveState.Activated)
        {
            if (inConstructionUnit == null)
            {
                state = ObjectiveState.Done;
                GetComponentInParent<IAManager>().CleanLists();
            }
            else if (inConstructionUnit.HasNoMoreBuilder())
            {
                inConstructionUnit.KillUnit();
                state = ObjectiveState.Done;
                GetComponentInParent<IAManager>().CleanLists();
            }
        }
    }

    public override void Activate()
    {
        SetBuilder();
        if (state == ObjectiveState.NeedBuilder || state == ObjectiveState.NeedWait || state == ObjectiveState.NeedPop)
            return;

        int res = GetComponentInParent<BotConstructionManager>().Construct(buildingIndex, builder, out inConstructionUnit);

        if (res != -1)
        {
            if (res == -3)
                state = ObjectiveState.NeedBuilding;
            else if (res == -2)
                state = ObjectiveState.NeedPop;
            else if (res == 0)
                state = ObjectiveState.NeedEnergy;
            else if (res == 1)
                state = ObjectiveState.NeedOre;
            else if (res == 2)
                state = ObjectiveState.NeedFood;
            else
                print("wtf");
        }
        else
        {
            base.Activate();
        }
    }
}

public enum BuildingUnits
{
        CombatStation,
        ConquestStation,
        EnergyFarm,
        Farm,
        House,
        Laboratory,
        Radar,
        TownHall,
        Turrel,
        None
}
