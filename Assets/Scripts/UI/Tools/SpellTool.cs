using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellTool : Tool
{
    Spell associatedSpell;

    public void Init(Spell spell)
    {
        associatedSpell = spell;
    }

    public void OnClicked()
    {
        PlayerController.playerController.InitSpellToolControls(associatedSpell);
    }
}
