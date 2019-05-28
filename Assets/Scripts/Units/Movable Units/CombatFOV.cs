using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatFOV : FieldOfViewCollider
{
    MovableUnit parent;
    public override void Init(Vector3 vec)
    {
        base.Init(vec);
        parent = GetComponentInParent<MovableUnit>();
    }

    protected override void OnStay(Collider collision)
    {
        if (collision == null || !parent.photonView.IsMine) return;
        base.OnStay(collision);
        if (parent != null && collision.GetComponent<DestructibleUnit>() != null && MultiplayerTools.GetTeamOf(parent) != MultiplayerTools.GetTeamOf(collision.GetComponent<DestructibleUnit>()))
            parent.OnEnemyEnters(collision.GetComponent<DestructibleUnit>());
    }
}
