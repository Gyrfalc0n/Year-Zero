using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CombatSystem))]
public class CombatUnit : MovableUnit
{
    CombatSystem combatSystem;

    private void Start()
    {
        combatSystem = GetComponent<CombatSystem>();
    }

    public void Attack(DestructibleUnit unit)
    {
        combatSystem.InitAttack(unit);
    }
}
