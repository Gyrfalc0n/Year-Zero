using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantationObjective : IAObjective
{
    public InstantiationUnit unit = InstantiationUnit.None;
    [HideInInspector]
    public int unitIndex;

    Task task;

    void Start()
    {
        if (unit != InstantiationUnit.None)
            unitIndex = (int)unit;
    }

    public void Init(int unitIndex)
    {
        unit = (InstantiationUnit)unitIndex;
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
        state = GetComponentInParent<BotInstantiationManager>().CreateUnit(unitIndex, out task);
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
