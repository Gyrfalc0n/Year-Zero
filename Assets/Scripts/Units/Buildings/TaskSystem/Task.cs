using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task : MonoBehaviour
{
    protected float requiredTime;
    protected float remainingTime;

    protected int[] costs;

    bool active;

    void Awake()
    {
        active = true;
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
        PlayerManager.playerManager.AddWood(costs[0]);
        PlayerManager.playerManager.AddStone(costs[1]);
        PlayerManager.playerManager.AddGold(costs[2]);
        PlayerManager.playerManager.AddMeat(costs[3]);
        PlayerManager.playerManager.AddPopulation(costs[4]);
        active = false;
    }

    public float GetCurrentAdvancement()
    {
        return 1 - remainingTime / requiredTime;
    }
}
