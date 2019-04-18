using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Photon.Pun;

public class CombatSystem : MonoBehaviour
{
    NavMeshAgent agent;
    DestructibleUnit target;

    string projectile = "BulletPrefab";
    Transform firePoint;
    Transform bulletHolder;

    [SerializeField]
    float range;

    public float attackRate;
    float time;

    private void Start()
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
        firePoint.localPosition = new Vector3(0.02f, -0.38f, 0.81f);
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

    private void Update()
    {
        if (time > 0)
        {
            time -= Time.deltaTime;
        }

        if (target != null)
        {
            if (Vector3.Distance(transform.position, target.transform.position) <= range)
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

    void Shoot()
    {
        if (time <= 0)
        {
            time = 1/attackRate;
            GameObject obj = PhotonNetwork.Instantiate("Units/Bullets/" + projectile, firePoint.position, firePoint.rotation);
            obj.transform.SetParent(bulletHolder);
            obj.GetComponent<Bullet>().Init(1f, GetComponent<MovableUnit>().damage, GetComponent<DestructibleUnit>());
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
