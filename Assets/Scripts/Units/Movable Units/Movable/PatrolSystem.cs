using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolSystem : MonoBehaviour
{
    NavMeshAgent agent;

    bool patroling;
    protected Vector3 pos1;
    protected Vector3 pos2;
    protected Vector3 lastPos;
    float stoppingDistance;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (patroling && Vector3.Distance(transform.position, agent.destination) <= agent.stoppingDistance)
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

    public void InitPatrol(Vector3 pos1, Vector3 pos2, float stoppingDistance)
    {
        patroling = true;
        this.pos1 = pos1;
        this.pos2 = pos2;
        this.stoppingDistance = stoppingDistance;
        lastPos = pos2;
        SetDestination(lastPos, stoppingDistance);
    }

    public void StopPatrol()
    {
        patroling = false;
        ResetDestination();
    }

    void OnReachedDestination()
    {
        lastPos = (lastPos == pos1) ? pos2 : pos1;
        SetDestination(lastPos, stoppingDistance);
    }

    public bool IsPatroling()
    {
        return patroling;
    }
}
