using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MobileMedicalStation : MovableUnit
{
    [SerializeField]
    float medicRequiredTime;
    float medicRemainingTime;
    [SerializeField]
    int lifeGiven;

    public override void Update()
    {
        medicRemainingTime -= Time.deltaTime;
    }

    void OnTriggerStay(Collider other)
    {
        if (medicRemainingTime <= 0)
        {
            medicRemainingTime = medicRequiredTime;
            if (other.GetComponent<MovableUnit>() != null && other.GetComponent<MovableUnit>().photonView.IsMine)
            {
                other.GetComponent<MovableUnit>().Heal(lifeGiven);
            }
        }

    }
}
