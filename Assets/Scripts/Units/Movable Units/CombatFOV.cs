using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFOV : MonoBehaviour
{
    MovableUnit parent;

    void Start()
    {
        parent = GetComponentInParent<MovableUnit>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (parent != null && other.GetComponent<DestructibleUnit>() != null && InstanceManager.instanceManager.IsEnemy(other.GetComponent<DestructibleUnit>()))
            parent.OnEnemyEnters(other.GetComponent<DestructibleUnit>());
    }
}
