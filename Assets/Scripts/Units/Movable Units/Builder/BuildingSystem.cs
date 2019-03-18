using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BuildingSystem : MonoBehaviour
{
    NavMeshAgent agent;

    CurrentBuildingAction currentAction;
    InConstructionUnit aimedBuilding;
    InConstructionUnit whatIsBuilding;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentAction = CurrentBuildingAction.nothing;
    }

    void Update()
    {
        if (currentAction == CurrentBuildingAction.goingToBuild && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance + 0.1f)
        {
            OnReachedDestination();
        }
        else if (currentAction == CurrentBuildingAction.building)
        {
            //StopBuilding();
        }
    }

    void SetDestination(Vector3 pos, float stoppingDistance)
    {
        agent.SetDestination(pos);
        agent.stoppingDistance = stoppingDistance;
    }

    void ResetDestination()
    {
        agent.ResetPath();
    }

    public void InitBuild(InConstructionUnit building)
    {
        if (currentAction == CurrentBuildingAction.building)
        {
            StopBuilding();
        }

        SetDestination(building.GetComponent<BoxCollider>().ClosestPoint(transform.position), 0.5f);
        aimedBuilding = building;
        currentAction = CurrentBuildingAction.goingToBuild;
    }

    public void StopBuilding()
    {
        if (currentAction == CurrentBuildingAction.building)
        {
            whatIsBuilding.RemoveBuilder(GetComponent<BuilderUnit>());
            whatIsBuilding = null;
        }
        else if (currentAction == CurrentBuildingAction.goingToBuild)
        {
            ResetDestination();
            aimedBuilding = null;
        }
        currentAction = CurrentBuildingAction.nothing;
    }

    private void OnReachedDestination()
    {
        currentAction = CurrentBuildingAction.building;
        aimedBuilding.AddBuilder(GetComponent<BuilderUnit>());
        whatIsBuilding = aimedBuilding;
        aimedBuilding = null;
    }

    public bool IsBuilding()
    {
        return !(currentAction == CurrentBuildingAction.nothing);
    }

    enum CurrentBuildingAction
    {
        nothing = 0,
        goingToBuild = 1,
        building = 2
    }
}


