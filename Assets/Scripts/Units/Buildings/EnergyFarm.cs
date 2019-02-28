using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnergyFarm : ConstructedUnit
{
    PlayerManager manager;

    int resourceIndex = 0;

    float timer;
    float timeReset = 1;

    readonly int value = 1;

    public override void Awake()
    {
        base.Awake();
        manager = PlayerManager.playerManager;
        timer = timeReset;
    }

    void Update()
    {
        AddResource();
    }

    void AddResource()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            manager.Add((int)(value * SkilltreeManager.manager.energyFarmSpeed), resourceIndex);
            timer = timeReset;
        }
    }
}
