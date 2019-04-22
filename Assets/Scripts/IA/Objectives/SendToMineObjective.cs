using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendToMineObjective : IAObjective
{
    public BuilderUnit builder;
    public int resourceIndex { get; private set; }
    bool forHouse;
    bool forBuilder;

    public void Init(int resourceIndex, bool forHouse, bool forBuilder)
    {
        this.forHouse = forHouse;
        this.forBuilder = forBuilder;
        this.resourceIndex = resourceIndex;
    }

    public void SetBuilder()
    {
        state = GetComponentInParent<BotBuilderManager>().GetOneBuilder(out builder, forHouse, forBuilder ,resourceIndex);
    }

    void Update()
    {
        if (state == ObjectiveState.Activated)
        {
            GetComponentInParent<BotBuilderManager>().DivideMiner();
            state = ObjectiveState.Done;
        }
    }

    public override void Activate()
    {
        SetBuilder();
        if (builder == null)
            return;

        GetComponentInParent<BotMiningManager>().SendToMine(builder, resourceIndex);
        base.Activate();
    }
}