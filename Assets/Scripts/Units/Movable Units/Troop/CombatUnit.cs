using System.Collections.Generic;
using UnityEngine;
using ExitGames.Client.Photon;

[RequireComponent(typeof(CombatSystem))]
public class CombatUnit : MovableUnit
{
    CombatSystem combatSystem;

    public override void Start()
    {
        base.Start();
        combatSystem = GetComponent<CombatSystem>();
    }

    public void Attack(DestructibleUnit unit)
    {
        combatSystem.InitAttack(unit);
    }

    public override void Interact(Interactable obj)
    {
        if (obj.GetComponent<DestructibleUnit>() != null)
        {
            Hashtable customProp = obj.photonView.Owner.CustomProperties;
            if ((int)customProp["Team"] == InstanceManager.instanceManager.GetTeam())
            {
                Attack(obj.GetComponent<DestructibleUnit>());
            }
        }
    }
}
