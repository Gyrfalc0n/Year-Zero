using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public override bool IsAvailable()
    {
        return SceneManager.GetActiveScene().name!="Tutorial";
    }
}
