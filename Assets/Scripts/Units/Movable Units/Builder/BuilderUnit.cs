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
            jobless.Add(this);
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
        if (obj.GetComponent<DestructibleUnit>() != null && MultiplayerTools.GetTeamOf(obj.GetComponent<DestructibleUnit>()) != MultiplayerTools.GetTeamOf(this))
        {
            Attack(obj.GetComponent<DestructibleUnit>());
        }
        else if (obj.GetComponent<InConstructionUnit>() != null && MultiplayerTools.GetTeamOf(obj.GetComponent<InConstructionUnit>()) == MultiplayerTools.GetTeamOf(this))
        {
            Build(obj.GetComponent<InConstructionUnit>());
        }
        else if (obj.GetComponent<ResourceUnit>() != null && (obj.GetComponent<ConstructedUnit>() == null || MultiplayerTools.GetTeamOf(obj.GetComponent<ConstructedUnit>()) == MultiplayerTools.GetTeamOf(this)))
        {
            Mine(obj.GetComponent<ResourceUnit>());
        }
        else if (obj.GetComponent<ConstructedUnit>() != null && MultiplayerTools.GetTeamOf(obj.GetComponent<ConstructedUnit>()) == MultiplayerTools.GetTeamOf(this) && obj.GetComponent<ConstructedUnit>().GetLife() < obj.GetComponent<ConstructedUnit>().GetMaxlife())
        {
            Repair(obj.GetComponent<ConstructedUnit>());
        }
    }

    public override void Attack(DestructibleUnit unit)
    {
        base.Attack(unit);
        if (jobless != null)
            jobless.Remove(this);
    }

    public void Build(InConstructionUnit obj)
    {
        ResetAction();
        buildingSystem.InitBuild(obj);
        if (jobless != null)
            jobless.Remove(this);
    }

    public void Mine(ResourceUnit obj)
    {
        home = (botIndex == -1) ? PlayerManager.playerManager.GetNearestHome(transform.position): InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotManager>().GetNearestHome(transform.position);
        ResetAction();
        miningSystem.InitMining(home, obj);
        if (jobless != null)
            jobless.Remove(this);
    }

    public void Repair(ConstructedUnit obj)
    {
        ResetAction();
        repairingSystem.InitRepair(obj);
        if (jobless != null)
            jobless.Remove(this);
    }

    public override void Patrol(Vector3 pos1, Vector3 pos2, float stoppingDistance)
    {
        ResetAction();
        base.Patrol(pos1, pos2, stoppingDistance);
        if (jobless != null)
            jobless.Remove(this);
    }

    public override void ResetAction()
    {
        if (IsDoingNothing()) return;

        base.ResetAction();
        if (miningSystem.IsMining())
            miningSystem.StoptMining();
        if (buildingSystem.IsBuilding())
            buildingSystem.StopBuilding();
        if (repairingSystem.IsRepairing())
            repairingSystem.StopRepairing();

        if (jobless != null)
            jobless.Add(this);
    }

    public bool IsDoingNothing()
    {
        bool immobile;
        immobile = (agent != null) ? Vector3.Distance(agent.destination, transform.position) <= 1:false;
        return immobile && IsDoingNothingExceptMoving();
    }

    public bool IsDoingNothingExceptMoving()
    {
        if (patrolSystem == null || miningSystem == null || buildingSystem == null || repairingSystem == null || combatSystem == null)
            return false;
        return (!patrolSystem.IsPatroling() && !miningSystem.IsMining() && !buildingSystem.IsBuilding() && !repairingSystem.IsRepairing() && !combatSystem.IsAttacking());
    }

    public bool IsMining()
    {
        return miningSystem.IsMining();
    }

    public bool IsBuilding()
    {
        return buildingSystem.IsBuilding();
    }

    public override void OnEnemyEnters(DestructibleUnit enemy)
    {
        if (IsDoingNothing())
        {
            combatSystem.OnEnemyEnters(enemy);
        }
    }

    public override void SetDestination(Vector3 pos, float stoppingDistance)
    {
        base.SetDestination(pos, stoppingDistance);
        if (jobless != null)
            jobless.Remove(this);
    }

    public override void OnReachedDestination()
    {
        base.OnReachedDestination();
        if (jobless != null)
            jobless.Add(this);
    }
}