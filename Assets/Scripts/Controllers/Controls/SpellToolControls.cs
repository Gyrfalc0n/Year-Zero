using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellToolControls : PlayerControls
{
    Spell associatedSpell;
    public LayerMask groundLayer;

    public void Init(Spell spell)
    {
        associatedSpell = spell;
    }

    public override void LeftClick()
    {
        CheckSpellZone();
        Cancel();
    }

    public void CheckSpellZone()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, Mathf.Infinity, groundLayer))
        {
            associatedSpell.Use();
        }
    }
}
