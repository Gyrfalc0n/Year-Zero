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
        if (associatedSpell.needSpellControls)
            PlayerController.playerController.InitSpellToolControls(associatedSpell);
        else
        {
            associatedSpell.Use();
        }
    }
}
