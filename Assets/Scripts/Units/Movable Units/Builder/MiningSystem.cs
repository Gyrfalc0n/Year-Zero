using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiningSystem : MonoBehaviour
{
    NavMeshAgent agent;

    protected Vector3 homePos;
    protected Vector3 resourcePos;
    protected Vector3 currentDestination;
    float stoppingDistance;

    bool mining = false;
    float resourceAmount;
    const int resourceMax = 50;
    private ResourceUnit currentResourceUnit;
    Resources lastMinedResource;
    private float speedMining = 2f;

    void Start()
    {
        lastMinedResource = Resources.NO;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (mining && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
            OnReachedDestination();
    }

    void SetDestination(Vector3 pos, float stoppingDistance)
    {
        agent.SetDestination(pos);
        agent.stoppingDistance = stoppingDistance;
    }

    public void InitPatrol(Vector3 pos1, Vector3 pos2, float stoppingDistance)
    {
        homePos = pos1;
        resourcePos = pos2;
        this.stoppingDistance = stoppingDistance;
        currentDestination = pos2;
        SetDestination(currentDestination, stoppingDistance);
    }
    public void StopPatrol()
    {
        ResetDestination();
    }

    void ResetDestination()
    {
        agent.ResetPath();
    }

    public void InitMining(TownHall home, ResourceUnit resourceUnit)
    {
        mining = true;
        currentResourceUnit = resourceUnit;
        InitPatrol(home.GetComponent<BoxCollider>().ClosestPoint(transform.position), currentResourceUnit.GetComponent<BoxCollider>().ClosestPoint(transform.position), 1f);
        if (resourceUnit.GetComponent<AsteroidResourceUnit>() != null)
        {
            lastMinedResource = Resources.ORE;
        }
        if (resourceUnit.GetComponent<FarmResourceUnit>() != null)
        {
            lastMinedResource = Resources.FOOD;
        }
    }

    public void StoptMining()
    {
        mining = false;
        currentResourceUnit = null;
        StopPatrol();
    }

    void OnReachedDestination()
    {
        if (currentDestination == resourcePos)
        {
            if (resourceAmount < resourceMax)
            {
                if (currentResourceUnit.StillResource())
                {
                    resourceAmount += currentResourceUnit.TakeResource(speedMining);
                }
                else
                {
                    GoBack();
                    if (currentResourceUnit != null)
                        currentResourceUnit.OnNoMoreResources();
                }
            }
            else
            {
                resourceAmount = resourceMax;
                GoBack();
                if (!currentResourceUnit.StillResource())
                    currentResourceUnit.OnNoMoreResources();
            }
        }
        else
        {
            GiveResources();
            GoBack();
        }
    }

    public bool IsMining()
    {
        return mining;
    }

    void GiveResources()
    {
        PlayerManager.playerManager.Add((int)resourceAmount, currentResourceUnit.GetResourceIndex());
        resourceAmount = 0;

    }

    void GoBack()
    {
        if (currentResourceUnit != null)
        {
            currentDestination = (currentDestination == homePos) ? resourcePos : homePos;
            SetDestination(currentDestination, stoppingDistance);
        }
        else
        {
            StoptMining();
        }
    }

    public float GetResourceAmount()
    {
        return resourceAmount;
    }

    public Resources GetLastResource()
    {
        return lastMinedResource;
    }

    public enum Resources
    {
        NO,
        ORE,
        FOOD
    }
}
