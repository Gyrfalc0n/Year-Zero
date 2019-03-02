using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;
using Photon.Realtime;

[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(PatrolSystem))]
[RequireComponent(typeof(CombatSystem))]
public class MovableUnit : DestructibleUnit {

    protected NavMeshAgent agent;
    public Card card;
    protected PatrolSystem patrolSystem;
    protected CombatSystem combatSystem;

    [HideInInspector]
    public TownHall home;

    [SerializeField]
    float requiredTime;

    [HideInInspector]
    public bool moving = false;

    public float defaultSpeed;
    [HideInInspector]
    public float speed;

    public override void Awake()
    {
        base.Awake();
        fieldOfViewPrefabPath = "VFX/FogOfWar/FieldOfViewPrefabForMV";
        defaultSpeed = 3.5f;
        speed = defaultSpeed;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        patrolSystem = GetComponent<PatrolSystem>();
        combatSystem = GetComponent<CombatSystem>();
        damage = defaultDamage;
        DetermineHome();
    }

    public virtual void Update()
    {
        if (moving)
        {
            if (Vector3.Distance(agent.destination, transform.position) <= agent.stoppingDistance)
            {
                OnReachedDestination();
            }
        }
    }

    public virtual float GetRequiredTime()
    {
        return requiredTime;
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

    public virtual void StopAttack()
    {
        combatSystem.StopAttack();
    }

    public virtual void ResetAction()
    {
        ResetDestination();
        if (patrolSystem.IsPatroling())
            StopPatrol();
        if (combatSystem.IsAttacking())
            StopAttack();
    }

    public override Vector3 GetSelectionCirclePos()
    {
        return new Vector3(0, -GetComponent<BoxCollider>().size.y / 2 + 0.01f, 0);
    }

    void OnReachedDestination()
    {
        moving = false;
        GameObject.Find("JoblessConstructorsPanel").GetComponent<JoblessConstructorsPanel>().UpdatePanel();
    }

    public void Hack()
    {
        if (photonView.IsMine || (int)photonView.Owner.CustomProperties["Team"] == InstanceManager.instanceManager.GetTeam())
            return;

        InstanceManager.instanceManager.mySelectableObjs.Add(this);
        photonView.RPC("RPCHacked", photonView.Owner);
        photonView.TransferOwnership(PhotonNetwork.LocalPlayer);
        selectionCircle.color = myColor;
    }

    [PunRPC]
    public void RPCHacked()
    {
        InstanceManager.instanceManager.mySelectableObjs.Remove(this);
        selectionCircle.color = enemyColor;
    }

    bool boosted;
    [HideInInspector]
    public float atkBoost;
    float lifeBoost;

    public void AddBoost(float atk, float life)
    {
        if (boosted)
            return;

        boosted = true;
        atkBoost = atk;
        lifeBoost = life;
        maxLife += (int)(defaultMaxLife * life);
    }

    public void RemoveBoost()
    {
        if (!boosted)
            return;

        boosted = false;
        atkBoost = 0;
        maxLife -= (int)(defaultMaxLife * lifeBoost);
    }


    public override void Interact(Interactable obj)
    {
        base.Interact(obj);
        if (obj.GetComponent<DestructibleUnit>() != null)
        {
            if (InstanceManager.instanceManager.IsEnemy(obj.photonView.Owner))
            {
                Attack(obj.GetComponent<DestructibleUnit>());
            }
        }
    }


    public float defaultDamage;
    [HideInInspector]
    public float damage;

    public void OnEnemyEnters(DestructibleUnit enemy)
    {
        if (!moving)
        {
            combatSystem.OnEnemyEnters(enemy);
        }
    }

    public void Attack(DestructibleUnit unit)
    {
        combatSystem.InitAttack(unit);
    }

    public override void OnDamageTaken(DestructibleUnit shooter)
    {
        base.OnDamageTaken(shooter);
        OnEnemyEnters(shooter);
    }
}
