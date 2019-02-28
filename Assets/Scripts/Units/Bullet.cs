using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks
{
    float speed;
    float damage;
    Rigidbody rb;

    public void Init(float speed, float damage)
    {
        this.speed = speed;
        this.damage = damage;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.GetComponent<DestructibleUnit>() != null && InstanceManager.instanceManager.IsEnemy(collision.collider.GetComponent<DestructibleUnit>().photonView.Owner))
        {
            collision.collider.GetComponent<DestructibleUnit>().TakeDamage((int)damage);
            PhotonNetwork.Destroy(this.photonView);
        }
    }
}
