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

    void OnTriggerStay(Collider other)
    {
        if (parent != null && other.GetComponent<DestructibleUnit>() != null && MultiplayerTools.GetTeamOf(parent) != MultiplayerTools.GetTeamOf(other.GetComponent<DestructibleUnit>()))
            parent.OnEnemyEnters(other.GetComponent<DestructibleUnit>());
    }
}
