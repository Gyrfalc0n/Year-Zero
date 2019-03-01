using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFOV : MonoBehaviour
{
    MovableUnit parent;

    private void Start()
    {
        parent = GetComponentInParent<MovableUnit>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (parent != null)
        {
            if (other.GetComponent<DestructibleUnit>() != null && InstanceManager.instanceManager.IsEnemy(other.GetComponent<DestructibleUnit>().photonView.Owner))
            {
                parent.OnEnemyEnters(other.GetComponent<DestructibleUnit>());
            }
        }
    }
}
