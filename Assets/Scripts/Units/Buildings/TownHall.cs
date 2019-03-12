using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TownHall : ProductionBuilding
{
    public override void Awake()
    {
        base.Awake();
        PlayerManager.playerManager.AddHome(this);
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
        PlayerManager.playerManager.RemoveHome(this);
    }
    
    public override bool IsAvailable()
    {
        return SceneManager.GetActiveScene().name!="Tutorial";
    }
}
