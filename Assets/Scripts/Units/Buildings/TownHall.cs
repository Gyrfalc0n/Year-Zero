using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownHall : ProductionBuilding
{
    public override void InitUnit(int botIndex)
    {
        base.InitUnit(botIndex);
        if (botIndex == -1)
        {
            PlayerManager.playerManager.AddHome(this);
            PlayerManager.playerManager.AddMaxPopulation(5);
        }
        else if (botIndex != -2)
        {
            InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotManager>().AddMaxPopulation(5);
        }
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
        PlayerManager.playerManager.RemoveHome(this);
        PlayerManager.playerManager.RemoveMaxPopulation(5);
    }
    
    public override bool IsAvailable()
    {
        return SceneManager.GetActiveScene().name!="Tutorial";
    }
}
