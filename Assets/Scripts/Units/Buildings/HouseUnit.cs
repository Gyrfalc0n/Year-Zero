using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseUnit : ConstructedUnit {

    [SerializeField]
    private int popValue;

    public override void Awake()
    {
        base.Awake();
        PlayerManager.playerManager.AddMaxPopulation(popValue);
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
        PlayerManager.playerManager.RemovePopulation(popValue);
    }
}
