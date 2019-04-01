using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantationObjective : IAObjective
{
    public InstantiationUnit unit = InstantiationUnit.None;
    [HideInInspector]
    public int unitIndex;

    InstantiateTask task;

    private void Start()
    {
        if (unit != InstantiationUnit.None)
            unitIndex = (int)unit;
    }

    public void Init(int unitIndex)
    {
        this.unitIndex = unitIndex;
    }

    void Update()
    {
        if (state == ObjectiveState.Activated && task == null)
        {
            state = ObjectiveState.Done;
        }
    }

    public override void Activate()
    {
        int res = GetComponentInParent<BotInstantiationManager>().CreateUnit(unitIndex, out task);
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
                print("gloubi");
        }
        else
        {
            base.Activate();
        }
    }
}

public enum InstantiationUnit
{
        BasicTroop,
        Bomber,
        Builder,
        Destroyer,
        Hacker,
        LightTroop,
        MobileMedicalStation,
        None
}
