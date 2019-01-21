using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MiningSystem))]
[RequireComponent(typeof(BuildingSystem))]
public class BuilderUnit : MovableUnit {

    public List<GameObject> buildings;

    //patrolSystem
    MiningSystem miningSystem;
    BuildingSystem buildingSystem;

    public override void Awake()
    {
        base.Awake();
        miningSystem = GetComponent<MiningSystem>();
        buildingSystem = GetComponent<BuildingSystem>();
    }

    public override void Interact(Interactable obj)
    {
        if (obj.GetComponent<InConstructionUnit>() != null)
        {
            Build(obj.GetComponent<InConstructionUnit>());
        }
        else if (obj.GetComponent<ResourceUnit>() != null && (obj.GetComponent<ConstructedUnit>() == null || obj.photonView.IsMine))
        {
            Mine(obj.GetComponent<ResourceUnit>());
        }
    }

    public void Build(InConstructionUnit obj)
    {
        ResetAction();
        buildingSystem.InitBuild(obj);
    }

    public void StopBuild()
    {
        buildingSystem.StopBuilding();
    }

    public void Mine(ResourceUnit obj)
    {
        ResetAction();
        miningSystem.InitMining(home, obj);
    }

    public void StopMine()
    {
        miningSystem.StoptMining();
    }

    public override void Patrol(Vector3 pos1, Vector3 pos2, float stoppingDistance)
    {
        ResetAction();
        base.Patrol(pos1, pos2, stoppingDistance);
    }

    public override void ResetAction()
    {
        base.ResetAction();
        if (miningSystem.IsMining())
            StopMine();
        if (buildingSystem.IsBuilding())
            StopBuild();
    }

    public bool IsDoingNothing()
    {
        return (!patrolSystem.IsPatroling() && !miningSystem.IsMining() && !buildingSystem.IsBuilding());
    }
}