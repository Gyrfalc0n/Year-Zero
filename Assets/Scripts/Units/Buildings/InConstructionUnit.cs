using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class InConstructionUnit : BuildingUnit
{
    ConstructedUnit associatedBuilding;

    float currentLife;

    private List<BuilderUnit> builders = new List<BuilderUnit>();

    void Update()
    {
        CheckConstruction();
    }

    public void Init(ConstructedUnit building)
    {
        associatedBuilding = building;
        currentLife = 0;
    }

    private void CheckConstruction()
    {
        if (currentLife < GetMaxlife())
        {
            currentLife += Time.deltaTime * builders.Count * SkilltreeManager.manager.constructionSpeed;
            SetLife(currentLife);
        }
        else
        {
            OnConstructionFinished();
        }
    }

    public virtual void OnConstructionFinished()
    {
        RemoveAllBuilders();
        InstanceManager.instanceManager.InstantiateUnit(associatedBuilding.GetPath(), transform.position, Quaternion.identity);
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
        PlayerManager.playerManager.Pay(associatedBuilding.costs, associatedBuilding.pop);
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
        return currentLife / GetMaxlife();
    }

    public ConstructedUnit GetAssociatedBuilding()
    {
        return associatedBuilding;
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(InConstructionUnit))]
public class InConstructionUnitEditor : Editor
{
    override public void OnInspectorGUI()
    {
    }
}
#endif
