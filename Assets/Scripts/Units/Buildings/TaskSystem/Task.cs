using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    protected float requiredTime;
    protected float remainingTime;

    protected ConstructedUnit associatedBuilding;

    protected bool active;

    public virtual void FirstInit(ConstructedUnit building)
    {
        associatedBuilding = building;
    }

    public void UpdateTask()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            OnFinishedTask();
        }
    }

    public virtual void OnFinishedTask()
    {
        active = false;
    }

    public bool Finished()
    {
        return !active;
    }

    public virtual void Cancel()
    {
        active = false;
    }

    public float GetCurrentAdvancement()
    {
        return 1 - remainingTime / requiredTime;
    }

    public ConstructedUnit GetBuilding()
    {
        return associatedBuilding;
    }
}
