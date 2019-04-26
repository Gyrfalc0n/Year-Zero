using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseUnit : ConstructedUnit {

    [SerializeField]
    private int popValue;

    public override void InitUnit(int botIndex)
    {
        base.InitUnit(botIndex);
        if (botIndex == -1)
            PlayerManager.playerManager.AddMaxPopulation(popValue);
        else
            InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotManager>().AddMaxPopulation(popValue);
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
        if (botIndex == -1)
        {
            PlayerManager.playerManager.RemoveMaxPopulation(popValue);
        }
        else if (botIndex != -2)
        {
            InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotManager>().RemoveMaxPopulation(popValue);
        }

    }
    public override bool IsAvailable()
    {
        return SceneManager.GetActiveScene().name!="Tutorial";
    }
}
