using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laboratory : ConstructedUnit
{
    PlayerManager manager;

    int resourceIndex = 3;

    float timer;
    float timeReset = 1;

    readonly int value = 1;

    public override void InitUnit(int botIndex)
    {
        base.InitUnit(botIndex);
        if (botIndex == -1)
            manager = PlayerManager.playerManager;
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
            manager.Add((int)(value * SkilltreeManager.manager.laboratorySpeed), resourceIndex);
            timer = timeReset;
        }
    }
    
    public override bool IsAvailable()
    {
        return SceneManager.GetActiveScene().name!="Tutorial";
    }
}
