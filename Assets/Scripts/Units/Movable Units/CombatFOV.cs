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
            if (other.GetComponent<DestructibleUnit>() != null)
            {
                if (parent.botIndex == -1)
                {
                    if (InstanceManager.instanceManager.IsEnemy(other.GetComponent<DestructibleUnit>()))
                    {
                        parent.OnEnemyEnters(other.GetComponent<DestructibleUnit>());
                    }
                }
                else
                {
                    if (InstanceManager.instanceManager.GetBot(parent.botIndex).IsEnemy(other.GetComponent<DestructibleUnit>()))
                    {
                        parent.OnEnemyEnters(other.GetComponent<DestructibleUnit>());
                    }
                }
            }
        }
    }
}
