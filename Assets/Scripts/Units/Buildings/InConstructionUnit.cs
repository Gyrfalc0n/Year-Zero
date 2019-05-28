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

    private List<BuilderUnit> builders = new List<BuilderUnit>();
    public int buildersCount { get; private set; }

    void Update()
    {
        if (!photonView.IsMine) return;
        CheckConstruction();
        fovCollider.enabled = ((int)GetLife() > 0);
        fovCollider.GetComponent<MeshRenderer>().enabled = ((int)GetLife() > 0); ;
    }

    public void Init(ConstructedUnit building)
    {
        maxLife = building.defaultMaxLife;
        associatedBuilding = building;
        buildersCount = 0;
    }

    void CheckConstruction()
    {
        if (InstanceManager.instanceManager.instantInstantiation)
            OnConstructionFinished();
        if (GetLife() < GetMaxlife())
        {
            Heal((int)(Time.deltaTime * builders.Count * SkilltreeManager.manager.constructionSpeed * 30 + buildersCount));
        }
        else
        {
            OnConstructionFinished();
        }
    }

    public virtual void OnConstructionFinished()
    {
        RemoveAllBuilders();
        InstanceManager.instanceManager.InstantiateUnit(associatedBuilding.GetPath(), transform.position, Quaternion.identity, botIndex);
        KillUnit();
    }

    public void AddBuilder(BuilderUnit builder)
    {
        hadBuilder = true;
        buildersCount++;
        builders.Add(builder);
    }

    public void RemoveBuilder(BuilderUnit builder)
    {
        buildersCount--;
        builders.Remove(builder);
    }

    public override void Cancel()
    {
        PlayerManager.playerManager.PayBack(associatedBuilding.costs, associatedBuilding.pop);
        KillUnit();
    }

    void RemoveAllBuilders()
    {
        for (int i = builders.Count-1; i >= 0; i--)
        {
            builders[i].ResetAction();
        }
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
        RemoveAllBuilders();
    }

    public ConstructedUnit GetAssociatedBuilding()
    {
        return associatedBuilding;
    }

    bool hadBuilder = false;
    public bool HasNoMoreBuilder()
    {
        return hadBuilder && builders.Count == 0;
    }

    public override float GetCurrentActionAdvancement()
    {
        return GetLife() / GetMaxlife();
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
