using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class InConstructionUnit : BuildingUnit
{
    ConstructedUnit associatedBuilding;

    float constructionTime;
    float remainingTime;

    public int lines;
    public int columns;

    private List<BuilderUnit> builders = new List<BuilderUnit>();

    void Update()
    {
        CheckConstruction();
    }

    public void Init(ConstructedUnit building)
    {
        associatedBuilding = building;
        constructionTime = building.GetRequiredTime();
        remainingTime = constructionTime;
    }

    private void CheckConstruction()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime * builders.Count;
        }
        else
        {
            OnConstructionFinished();
        }
    }

    public virtual void OnConstructionFinished()
    {
        RemoveAllBuilders();
        InstanceManager.instanceManager.InstantiateUnit(MyTools.GetPath(associatedBuilding.gameObject), transform.position, Quaternion.identity);
        KillUnit();
    }

    public void AddBuilder(BuilderUnit builder)
    {
        builders.Add(builder);
    }

    public void RemoveBuilder(BuilderUnit builder)
    {
        builders.Remove(builder);
    }

    public override void Cancel()
    {
        PlayerManager.playerManager.Pay(associatedBuilding.costs);
        KillUnit();
    }

    void RemoveAllBuilders()
    {
        for (int i = builders.Count-1; i >= 0; i--)
        {
            builders[i].StopBuild();
        }
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
        RemoveAllBuilders();
    }

    public override float GetCurrentActionAdvancement()
    {
        return 1 - remainingTime / constructionTime;
    }
}
