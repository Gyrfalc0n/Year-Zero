using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToMineObjective : IAObjective
{
    public BuilderUnit builder;
    public int resourceIndex { get; private set; }

    public void Init(int resourceIndex)
    {
        this.resourceIndex = resourceIndex;
    }

    public void SetBuilder()
    {
        state = GetComponentInParent<BotBuilderManager>().GetOneBuilder(out builder, false, true);
    }

    void Update()
    {
        if (state == ObjectiveState.Activated)
        {
            state = ObjectiveState.Done;
        }
    }

    public override void Activate()
    {
        SetBuilder();
        if (state == ObjectiveState.NeedBuilder || state == ObjectiveState.Done)
            return;

        GetComponentInParent<BotMiningManager>().SendToMine(builder, resourceIndex);
        base.Activate();
    }
}