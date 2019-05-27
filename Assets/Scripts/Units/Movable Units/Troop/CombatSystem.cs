using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class CombatSystem : MonoBehaviour
{
    NavMeshAgent agent;
    DestructibleUnit target;

    [SerializeField]
    string projectile = "BulletPrefab";
    Transform firePoint;
    Transform bulletHolder;

    public float range;
    public float attackRate;
    [SerializeField]
    int bulletAmount;
    int _bulletAmount;
    float waveTime;

    void Start()
    {
        bulletHolder = InstanceManager.instanceManager.bulletHolder;
        InitFirePoint();
        agent = GetComponent<NavMeshAgent>();
        target = null;
    }

    void InitFirePoint()
    {
        firePoint = new GameObject().transform;
        firePoint.SetParent(transform);
        firePoint.localPosition = new Vector3(0.02f, 0f, 0.81f);
        firePoint.gameObject.name = "FirePoint";
    }

    public void InitAttack(DestructibleUnit unit)
    {
        target = unit;
    }

    public void StopAttack()
    {
        target = null;
    }

    void Update()
    {
        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= range * 7)
            {
                FaceTarget(target.transform.position);
                agent.ResetPath();
                Shoot();
            }
            else
            {
                agent.SetDestination(target.transform.position);
            }
        }
    }

    float timeBetweenBullets;
    void Shoot()
    {
        if (waveTime > 0)
        {
            waveTime -= Time.deltaTime;
        }
        if (waveTime <= 0)
        {
            if (timeBetweenBullets <= 0)
            {
                timeBetweenBullets = 0.3f;
                if (_bulletAmount > 0)
                {
                    _bulletAmount--;
                    GameObject obj = PhotonNetwork.Instantiate("Units/Bullets/" + projectile, firePoint.position, firePoint.rotation);
                    obj.transform.SetParent(bulletHolder);
                    obj.GetComponent<Bullet>().Init(14f, GetComponent<MovableUnit>().damage, GetComponent<DestructibleUnit>());
                }
                else
                {
                    waveTime = 1 / attackRate;
                    _bulletAmount = bulletAmount;
                }
            }
            else
            {
                timeBetweenBullets -= Time.deltaTime;
            }
        }
    }

    public void OnEnemyEnters(DestructibleUnit enemy)
    {
        if (target == null)
        {
            target = enemy;
        }
    }

    public bool IsAttacking()
    {
        return target != null;
    }

    void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);
    }
}
