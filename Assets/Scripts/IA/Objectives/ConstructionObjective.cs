using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionObjective : IAObjective
{
    public BuilderUnit builder { get; private set; }
    public int buildingIndex;

    public void Init(int buildingIndex)
    {
        returnValue = -1;
        this.buildingIndex = buildingIndex;
    }

    public void SetBuilder()
    {
        BuilderUnit builder = GetComponentInParent<IAManager>().GetJoblessBuilder();
        if (builder != null)
            this.builder = builder;
        else
            returnValue = 1;
    }

    public override void Activate()
    {
        base.Activate();
        returnValue = GetComponentInParent<BotConstructionManager>().Construct(buildingIndex, builder);
    }
}
