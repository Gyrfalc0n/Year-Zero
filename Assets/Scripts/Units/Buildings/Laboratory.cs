using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laboratory : ConstructedUnit
{
    PlayerManager manager;

    int resourceIndex = 3;

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
            manager.Add(value, resourceIndex);
            timer = timeReset;
        }
    }
}
