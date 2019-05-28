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
                Destroy();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (photonView.IsMine && associatedUnit != null)
        {
            if (other.GetComponent<DestructibleUnit>() != null)
            {
                if (MultiplayerTools.GetTeamOf(associatedUnit) != MultiplayerTools.GetTeamOf(other.GetComponent<DestructibleUnit>()))
                {
                    other.GetComponent<DestructibleUnit>().TakeDamage((int)damage, associatedUnit);
                    Destroy();
                }
            }
        }
    }

    [SerializeField] string destructionAnimation;
    public void Destroy()
    {
        PhotonNetwork.Instantiate("Units/Bullets/DestructionAnimations/" + destructionAnimation, transform.position, Quaternion.identity);
        PhotonNetwork.Destroy(this.gameObject);
    }
}
