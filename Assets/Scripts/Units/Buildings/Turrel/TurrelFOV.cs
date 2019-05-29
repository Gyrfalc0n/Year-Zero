using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class TurrelFOV : MonoBehaviour
{
    DestructibleUnit target;

    [HideInInspector]
    public float defaultRange;
    
    public float defaultDamage;
    [HideInInspector]
    public float damage;

    private void Start()
    {
        firePoint = GetComponentInParent<Turrel>().firePoint;
        damage = defaultDamage * SkilltreeManager.manager.turrelBonusDamage;
        defaultRange = transform.localScale.x;
        SetRange(SkilltreeManager.manager.turrelRange);
    }

    public void SetRange(float vec)
    {
        Vector3 tmp = new Vector3(defaultRange, transform.localScale.y, defaultRange);
        tmp.x *= vec;
        tmp.z *= vec;
        transform.localScale = tmp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null)
        {
            if (other.GetComponent<DestructibleUnit>() != null &&  MultiplayerTools.GetTeamOf(other.GetComponent<SelectableObj>()) != MultiplayerTools.GetTeamOf(GetComponentInParent<Turrel>()))
            {
                if (GetComponentInParent<Turrel>().turretRotation != null)
                {
                    target = other.GetComponent<DestructibleUnit>();
                    GetComponentInParent<Turrel>().turretRotation.SetTarget(target.transform);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DestructibleUnit>() == target && target != null)
        {
            GetComponentInParent<Turrel>().turretRotation.SetTarget(null);
            target = null;
        }
    }

    private void Update()
    {
        if (target != null)
        {
            Shoot();
        }
    }

    [SerializeField]
    string projectile;
    [SerializeField]
    Transform firePoint;

    public float attackRate;
    float time;

    void Shoot()
    {
        if (time <= 0)
        {
            time = attackRate;
            GameObject obj = PhotonNetwork.Instantiate("Units/Bullets/" + projectile, firePoint.position, firePoint.rotation);
            obj.GetComponent<Bullet>().Init(500, damage, GetComponentInParent<Turrel>());
        }
        time -= Time.deltaTime;
    }

    void FaceTarget(Vector3 destination)
    {
        Vector3 lookPos = destination - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.1f);
    }
}
