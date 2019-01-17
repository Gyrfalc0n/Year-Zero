using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiningSystem : MonoBehaviour
{
    NavMeshAgent agent;

    protected Vector3 pos1;
    protected Vector3 pos2;
    protected Vector3 lastPos;
    float stoppingDistance;

    bool mining = false;
    private float resourceAmount;
    const int resourceMax = 50;
    private ResourceUnit currentResourceUnit;
    private float speedMining = 2f;

    private void Start()
    {
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
        this.pos1 = pos1;
        this.pos2 = pos2;
        this.stoppingDistance = stoppingDistance;
        lastPos = pos2;
        SetDestination(lastPos, stoppingDistance);
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
    }

    public void StoptMining()
    {
        mining = false;
        currentResourceUnit = null;
        StopPatrol();
    }

    void OnReachedDestination()
    {
        if (lastPos == pos2)
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
                    currentResourceUnit.Destroy();
                }
            }
            else
            {
                resourceAmount = resourceMax;
                GoBack();
                if (!currentResourceUnit.StillResource())
                    currentResourceUnit.Destroy();
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
            lastPos = (lastPos == pos1) ? pos2 : pos1;
            SetDestination(lastPos, stoppingDistance);
        }
        else
        {
            StoptMining();
        }
    }
}
