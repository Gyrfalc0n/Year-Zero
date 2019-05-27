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


    public override void InitUnit(int botIndex)
    {
        fieldOfViewPrefabPath = "VFX/FogOfWar/FieldOfViewPrefabForMV";
        base.InitUnit(botIndex);
        defaultSpeed = 3.5f;
        speed = defaultSpeed;
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speed;
        patrolSystem = GetComponent<PatrolSystem>();
        combatSystem = GetComponent<CombatSystem>();
        damage = defaultDamage;
        DetermineHome();
        if (botIndex != -1 && botIndex != -2 && GetComponent<BuilderUnit>() == null)
        {
            InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotArmyManager>().Add(this);
        }
        
        if (botIndex == -1 && photonView.IsMine && InstanceManager.instanceManager.timer > 1) 
        {
            FindObjectOfType<AudioManager>().PlaySound("UnitSpawn");
        }
    }

    [PunRPC]
    public override void RPCInitUnit(int botIndex)
    {
        fieldOfViewPrefabPath = "VFX/FogOfWar/FieldOfViewPrefabForMV";
        base.RPCInitUnit(botIndex);
    }

    protected bool alwaysAttack = false;

    public virtual void Update()
    {
        if (alwaysAttack && !GetComponent<CombatSystem>().IsAttacking())
        {
            DestructibleUnit target;
            if (botIndex == -2)
            {
                target = GameObject.Find("independantBotPrefab").GetComponent<BotArmyManager>().GetNearestEnemy();
            }
            else
            {
                target = InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotArmyManager>().GetNearestEnemy();
            }
            if (target == null) return;
            Attack(target, false);
        }
        else if (moving)
        {
            if (Vector3.Distance(agent.destination, transform.position) <= agent.stoppingDistance)
            {
                OnReachedDestination();
            }
        }
    }

    public override string GetPath()
    {
        return "Units/" + name;
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
        moving = true;
    }

    void DetermineHome()
    {
        if (botIndex == -2)
            return;

        List<TownHall> tmpHomes;
        tmpHomes = (botIndex == -1) ? PlayerManager.playerManager.GetHomes() : InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotManager>().GetHomes();

        TownHall nearest = PlayerManager.playerManager.GetHomes()[0];
        foreach (TownHall townHall in tmpHomes)
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

    public virtual void ResetAction()
    {
        if (agent == null) return;
        if (agent.hasPath)
            agent.ResetPath();
        if (patrolSystem.IsPatroling())
            patrolSystem.StopPatrol();
        if (combatSystem.IsAttacking())
            combatSystem.StopAttack();
    }

    public override Vector3 GetSelectionCirclePos()
    {
        float tmp = (GetComponent<SphereCollider>() != null) ? GetComponent<SphereCollider>().radius : GetComponent<BoxCollider>().size.y / 2;

        return new Vector3(0, -tmp, 0);
    }

    public virtual void OnReachedDestination()
    {
        moving = false;
        attackMove = false;
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
        if (obj.GetComponent<DestructibleUnit>() != null && MultiplayerTools.GetTeamOf(obj.GetComponent<DestructibleUnit>()) != MultiplayerTools.GetTeamOf(this))
        {
            Attack(obj.GetComponent<DestructibleUnit>(), false);
        }
    }

    public float defaultDamage;
    [HideInInspector]
    public float damage;

    bool attackMove = false;
    public virtual void OnEnemyEnters(DestructibleUnit enemy)
    {
        if (attackMove || !moving && !combatSystem.IsAttacking())
        {
            ResetAction();
            combatSystem.OnEnemyEnters(enemy);
        }
    }

    public virtual void Attack(DestructibleUnit unit, bool attackMove)
    {
        this.attackMove = attackMove;
        audioManager.PlaySound("AttackCommand");
        combatSystem.InitAttack(unit);
    }

    public override void OnDamageTaken(DestructibleUnit shooter)
    {
        base.OnDamageTaken(shooter);
        OnEnemyEnters(shooter);
    }

    public void SetAlwaysAttack()
    {
        alwaysAttack = true;
    }

    public override void OnDestroyed()
    {
        base.OnDestroyed();
        if (GetComponent<BuilderUnit>() == null)
        {
            if (botIndex != -1 && botIndex != -2)
            {
                InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotManager>().RemovePopulation(pop);
                InstanceManager.instanceManager.GetBot(botIndex).GetComponent<BotArmyManager>().Remove(this);
            }
            else if (botIndex == -2)
            {
                GameObject.Find("independantBotPrefab").GetComponent<BotArmyManager>().Remove(this);
            }
            else
            {
                PlayerManager.playerManager.RemovePopulation(pop);
            }
        }
    }
}
