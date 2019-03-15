using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnergyFarm : ConstructedUnit
{
    PlayerManager manager;

    int resourceIndex = 0;

    float timer;
    float timeReset = 1;

    readonly int value = 1;

    public override void InitUnit(int botIndex)
    {
        base.InitUnit(botIndex);
        if (botIndex == -1)
        {
            manager = PlayerManager.playerManager;
        }
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
    
    public override bool IsAvailable()
    {
        return SceneManager.GetActiveScene().name!="Tutorial";
    }
}
