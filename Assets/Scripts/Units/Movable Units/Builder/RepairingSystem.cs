using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RepairingSystem : MonoBehaviour
{
    NavMeshAgent agent;

    CurrentRepairinggAction currentAction;
    ConstructedUnit aimedBuilding;
    ConstructedUnit whatIsRepairing;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        currentAction = CurrentRepairinggAction.nothing;
    }

    void Update()
    {
        if (currentAction == CurrentRepairinggAction.goingToRepair && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
        {
            OnReachedDestination();
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

    public void InitRepair(ConstructedUnit building)
    {
        if (currentAction == CurrentRepairinggAction.repairing)
        {
            StopRepairing();
        }

        SetDestination(building.GetComponent<BoxCollider>().ClosestPoint(transform.position), 0.5f);
        aimedBuilding = building;
        currentAction = CurrentRepairinggAction.goingToRepair;
    }

    public void StopRepairing()
    {
        if (currentAction == CurrentRepairinggAction.repairing)
        {
            whatIsRepairing.RemoveRepairer(GetComponent<BuilderUnit>());
            whatIsRepairing = null;
        }
        else if (currentAction == CurrentRepairinggAction.goingToRepair)
        {
            ResetDestination();
            aimedBuilding = null;
        }
        currentAction = CurrentRepairinggAction.nothing;
    }

    private void OnReachedDestination()
    {
        currentAction = CurrentRepairinggAction.repairing;
        aimedBuilding.AddRepairer(GetComponent<BuilderUnit>());
        whatIsRepairing = aimedBuilding;
        aimedBuilding = null;
    }

    public bool IsRepairing()
    {
        return !(currentAction == CurrentRepairinggAction.nothing);
    }

    enum CurrentRepairinggAction
    {
        nothing = 0,
        goingToRepair = 1,
        repairing = 2
    }
}
