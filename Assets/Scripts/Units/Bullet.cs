using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks
{
    DestructibleUnit associatedUnit;
    float speed;
    float damage;
    Rigidbody rb;
    float time;

    public void Init(float speed, float damage, DestructibleUnit unit)
    {
        associatedUnit = unit;
        this.speed = speed;
        this.damage = damage;
    }

    void Start()
    {
        if (photonView.IsMine)
        {
            time = 5f;
            rb = GetComponent<Rigidbody>();
            rb.velocity = transform.forward * speed;
        }
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            time -= Time.deltaTime;
            if (time <= 0)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (photonView.IsMine)
        {
            if (other.GetComponent<DestructibleUnit>() != null && InstanceManager.instanceManager.IsEnemy(other.GetComponent<DestructibleUnit>().photonView.Owner))
            {
                other.GetComponent<DestructibleUnit>().TakeDamage((int)damage, associatedUnit);
                PhotonNetwork.Destroy(this.gameObject);
            }
        }
    }
}
