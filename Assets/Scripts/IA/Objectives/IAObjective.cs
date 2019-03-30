using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAObjective : MonoBehaviour
{
    public ObjectiveState state = ObjectiveState.Deactivated;

    public virtual void Activate()
    {
        if (state == ObjectiveState.Deactivated)
        {
            state = ObjectiveState.Activated;
        }
    }

    public virtual void Deactivate()
    {
        state = ObjectiveState.Deactivated;
    }

    public bool IsActivated()
    {
        return state == ObjectiveState.Activated;
    }

    public bool Finished()
    {
        return state == ObjectiveState.Done;
    }
}

public enum ObjectiveState
{
    Activated,
    Done,
    NeedBuilder,
    NeedBuilding,
    NeedEnergy,
    NeedOre,
    NeedFood,
    NeedPop,
    NeedWait,
    SuicideTroop,
    Deactivated
}
