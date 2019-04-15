using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnergyFarm : ConstructedUnit
{
    PlayerManager manager;
    BotManager botManager;

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
        else
        {
            botManager = InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotManager>();
        }
        timer = timeReset;
    }

    void Update()
    {
        if (photonView.IsMine)
            AddResource();
    }

    void AddResource()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (botIndex == -1)
                manager.Add((int)(value * SkilltreeManager.manager.energyFarmSpeed), resourceIndex);
            else
                botManager.Add((int)(value * SkilltreeManager.manager.energyFarmSpeed), resourceIndex);
            timer = timeReset;
        }
    }
    
    public override bool IsAvailable()
    {
        return SceneManager.GetActiveScene().name!="Tutorial";
    }
}
