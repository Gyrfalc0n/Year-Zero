using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MiningSystem))]
[RequireComponent(typeof(BuildingSystem))]
[RequireComponent(typeof(RepairingSystem))]
public class BuilderUnit : MovableUnit {

    public List<GameObject> buildings;

    //patrolSystem
    MiningSystem miningSystem;
    BuildingSystem buildingSystem;
    RepairingSystem repairingSystem;
    JoblessConstructorsPanel jobless;

    public override void InitUnit(int botIndex)
    {
        base.InitUnit(botIndex);
        miningSystem = GetComponent<MiningSystem>();
        buildingSystem = GetComponent<BuildingSystem>();
        repairingSystem = GetComponent<RepairingSystem>();
        if (botIndex == -1)
        {
            jobless = GameObject.Find("JoblessConstructorsPanel").GetComponent<JoblessConstructorsPanel>();
            UpdateJoblessPanel();
        }
        else if (botIndex != -2)
        {
            InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotBuilderManager>().Add(this);
        }
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
        if (botIndex != -1 && botIndex != -2)
        {
            InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotBuilderManager>().Remove(this);
        }
    }

    public override void Interact(Interactable obj)
    {
        base.Interact(obj);
        if (obj.GetComponent<InConstructionUnit>() != null)
        {
            Build(obj.GetComponent<InConstructionUnit>());
        }
        else if (obj.GetComponent<ResourceUnit>() != null && (obj.GetComponent<ConstructedUnit>() == null || obj.photonView.IsMine))
        {
            Mine(obj.GetComponent<ResourceUnit>());
        }
        else if (obj.GetComponent<ConstructedUnit>() != null && MultiplayerTools.GetTeamOf(obj.GetComponent<ConstructedUnit>()) == MultiplayerTools.GetTeamOf(this) && obj.GetComponent<ConstructedUnit>().GetLife() < obj.GetComponent<ConstructedUnit>().GetMaxlife())
        {
            Repair(obj.GetComponent<ConstructedUnit>());
        }
        UpdateJoblessPanel();
    }

    public void Build(InConstructionUnit obj)
    {
        ResetAction();
        buildingSystem.InitBuild(obj);
        UpdateJoblessPanel();
    }

    public void StopBuild()
    {
        buildingSystem.StopBuilding();
        UpdateJoblessPanel();
    }

    public void Mine(ResourceUnit obj)
    {
        if (botIndex == -1)
        {
            home = PlayerManager.playerManager.GetNearestHome(transform.position);
        }
        else
        {
            home = InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotManager>().GetNearestHome(transform.position);
        }

        ResetAction();
        miningSystem.InitMining(home, obj);
        UpdateJoblessPanel();
    }

    public void StopMine()
    {
        miningSystem.StoptMining();
        UpdateJoblessPanel();
    }

    public void Repair(ConstructedUnit obj)
    {
        ResetAction();
        repairingSystem.InitRepair(obj);
        UpdateJoblessPanel();
    }

    public void StopRepairing()
    {
        repairingSystem.StopRepairing();
        UpdateJoblessPanel();
    }

    public override void Patrol(Vector3 pos1, Vector3 pos2, float stoppingDistance)
    {
        ResetAction();
        base.Patrol(pos1, pos2, stoppingDistance);
        UpdateJoblessPanel();
    }

    public override void ResetAction()
    {
        base.ResetAction();
        if (miningSystem.IsMining())
            StopMine();
        if (buildingSystem.IsBuilding())
        {
            StopBuild();
        }

        UpdateJoblessPanel();
    }

    public bool IsDoingNothing()
    {
        bool immobile;
        immobile = (agent != null) ? Vector3.Distance(agent.destination, transform.position) <= 1:false;
        return immobile && IsDoingNothingExceptMoving();
    }

    public bool IsDoingNothingExceptMoving()
    {
        if (patrolSystem == null || miningSystem == null || buildingSystem == null || repairingSystem == null)
            return false;
        return (!patrolSystem.IsPatroling() && !miningSystem.IsMining() && !buildingSystem.IsBuilding() && !repairingSystem.IsRepairing());
    }

    public bool IsMining()
    {
        return miningSystem.IsMining();
    }

    public bool IsBuilding()
    {
        return buildingSystem.IsBuilding();
    }

    public void UpdateJoblessPanel()
    {
        if (botIndex == -1)
            jobless.UpdatePanel();
    }

    public override void OnEnemyEnters(DestructibleUnit enemy)
    {
        if (IsDoingNothing())
        {
            combatSystem.OnEnemyEnters(enemy);
        }
    }
}