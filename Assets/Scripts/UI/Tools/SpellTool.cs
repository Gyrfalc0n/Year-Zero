using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellTool : Tool
{
    Spell associatedSpell;
    RadialLoadingBar bar;

    public void Init(Spell spell)
    {
        associatedSpell = spell;
        bar = GetComponentInChildren<RadialLoadingBar>();
    }

    public void OnClicked()
    {
        if (associatedSpell.needSpellControls)
        {
            PlayerController.playerController.InitSpellToolControls(associatedSpell);
        }
        else
        {
            associatedSpell.Use();
        }
    }

    private void Update()
    {
        if (associatedSpell.GetRemainingTime() <= 0)
        {
            bar.gameObject.SetActive(false);
            GetComponent<Button>().interactable = true;
        }
        else
        {
            GetComponent<Button>().interactable = false;
            bar.gameObject.SetActive(true);
            bar.Set(associatedSpell.GetRemainingTime() / associatedSpell.GetRequiredTime());
        }
    }
}
