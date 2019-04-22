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
    bool forHouse;
    bool forBuilder;

    InConstructionUnit inConstructionUnit;

    private void Start()
    {
        if (buildingUnits != BuildingUnits.None)
            buildingIndex = (int)buildingUnits;
        else
            buildingUnits = (BuildingUnits)buildingIndex;
    }

    public void Init(int buildingIndex, bool forHouse, bool forBuilder)
    {
        state = ObjectiveState.Deactivated;
        this.buildingIndex = buildingIndex;
        this.forHouse = forHouse;
        this.forBuilder = forBuilder;
    }

    public void SetBuilder()
    {
        state = GetComponentInParent<BotBuilderManager>().GetOneBuilder(out builder, forHouse, forBuilder);
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
        state = GetComponentInParent<BotConstructionManager>().CanConstruct(buildingIndex);
        if (state != ObjectiveState.Activated)
            return;

        SetBuilder();
        if (builder == null)
            return;

        GetComponentInParent<BotConstructionManager>().Construct(buildingIndex, builder, out inConstructionUnit);
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
