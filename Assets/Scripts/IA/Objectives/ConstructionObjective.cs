using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionObjective : IAObjective
{
    public BuilderUnit builder;
    public int buildingIndex;

    InConstructionUnit inConstructionUnit;

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
            }
            else if (inConstructionUnit.HasNoMoreBuilder())
            {
                inConstructionUnit.KillUnit();
                state = ObjectiveState.Done;
            }
        }
    }

    public override void Activate()
    {
        print(buildingIndex);
        SetBuilder();
        print(state);
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
