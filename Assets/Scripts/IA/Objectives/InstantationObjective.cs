using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantationObjective : IAObjective
{
    public int unitIndex;
    public int amount;

    public void Init(int unitIndex, int amount)
    {
        returnValue = -1;
        this.unitIndex = unitIndex;
        this.amount = amount;
    }

    public override void Activate()
    {
        base.Activate();
        returnValue = GetComponentInParent<BotInstantiationManager>().CreateUnit(unitIndex, amount);
    }
}
