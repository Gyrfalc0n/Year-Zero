using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PatrolSystem))]
public class MovableUnit : DestructibleUnit {

    protected NavMeshAgent agent;
    public Card card;
    protected PatrolSystem patrolSystem;

    [HideInInspector]
    public TownHall home;

    bool moving = false;

    public override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        patrolSystem = GetComponent<PatrolSystem>();
        DetermineHome();
    }

    public virtual void Update()
    {
        if (moving)
        {
            if (Vector3.Distance(agent.destination, transform.position) <= agent.stoppingDistance)
            {
                moving = false;
                OnReachedDestination();
            }
        }
    }

    public void Init(Vector3 vec)
    {
        SetDestination(vec, 1);
    }
    
    public virtual void SetDestination(Vector3 pos, float stoppingDistance)
    {
        ResetAction();
        agent.SetDestination(pos);
        agent.stoppingDistance = stoppingDistance;
        GameObject.Find("JoblessConstructorsPanel").GetComponent<JoblessConstructorsPanel>().UpdatePanel();
        moving = true;
    }

    public void ResetDestination()
    {
        agent.ResetPath();
    }

    void DetermineHome()
    {
        TownHall nearest = PlayerManager.playerManager.GetHomes()[0];
        foreach (TownHall townHall in PlayerManager.playerManager.GetHomes())
        {
            if (Vector3.Distance(townHall.transform.position, transform.position) < (Vector3.Distance(nearest.transform.position, transform.position)))
            {
                nearest = townHall;
            }
        }
        home = nearest;
    }

    public virtual void Patrol(Vector3 pos1, Vector3 pos2, float stoppingDistance)
    {
        patrolSystem.InitPatrol(pos1, pos2, stoppingDistance);
    }

    public bool IsPatroling()
    {
        return patrolSystem.IsPatroling();
    }

    public virtual void StopPatrol()
    {
        patrolSystem.StopPatrol();
    }

    public virtual void ResetAction()
    {
        ResetDestination();
        if (patrolSystem.IsPatroling())
            StopPatrol();
    }

    public override Vector3 GetSelectionCirclePos()
    {
        return new Vector3(0, -GetComponent<BoxCollider>().size.y / 2 + 0.01f, 0);
    }

    void OnReachedDestination()
    {
        GameObject.Find("JoblessConstructorsPanel").GetComponent<JoblessConstructorsPanel>().UpdatePanel();
    }
}
