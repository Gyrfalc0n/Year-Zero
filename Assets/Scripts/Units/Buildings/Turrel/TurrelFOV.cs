using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        damage = defaultDamage + SkilltreeManager.manager.turrelDamage;
        defaultRange = transform.localScale.x;
        SetRange(SkilltreeManager.manager.turrelRange);
    }

    public void SetRange(float vec)
    {
        Vector3 tmp = new Vector3(defaultRange, transform.localScale.y, defaultRange);
        tmp.x += vec;
        tmp.z += vec;
        transform.localScale = tmp;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (target == null)
        {
            if (other.GetComponent<DestructibleUnit>() != null && !InstanceManager.instanceManager.IsEnemy(other.GetComponent<DestructibleUnit>().photonView.Owner))
            {
                target = other.GetComponent<DestructibleUnit>();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<DestructibleUnit>() == target && target != null)
        {
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

    void Shoot()
    {
        //SHOOT
    }
}
