using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bullet : MonoBehaviourPunCallbacks
{
    DestructibleUnit associatedUnit;
    float damage;
    float time;

    public void Init(float speed, float damage, DestructibleUnit unit)
    {
        time = 5f;
        associatedUnit = unit;
        this.damage = damage;
        SetForce(transform.forward * speed);
        if (!PhotonNetwork.OfflineMode)
            photonView.RPC("SetForce", RpcTarget.Others, transform.forward * speed);
    }

    [PunRPC]
    public void SetForce(Vector3 force)
    {
        GetComponent<Rigidbody>().velocity = force;
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
            if (other.GetComponent<DestructibleUnit>() != null)
            {
                if (associatedUnit.botIndex == -1 && InstanceManager.instanceManager.IsEnemy(other.GetComponent<DestructibleUnit>()) || associatedUnit.botIndex != -1 && InstanceManager.instanceManager.GetBot(associatedUnit.botIndex).IsEnemy(other.GetComponent<DestructibleUnit>()))
                {
                    other.GetComponent<DestructibleUnit>().TakeDamage((int)damage, associatedUnit);
                    PhotonNetwork.Destroy(this.gameObject);
                }
            }
        }
    }
}
