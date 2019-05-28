using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellTool : Tool
{
    Spell associatedSpell;
    RadialLoadingBar bar;
    [SerializeField] Image image;

    public void Init(Spell spell)
    {
        associatedSpell = spell;
        bar = GetComponentInChildren<RadialLoadingBar>();
        SetButtonSprite(spell);
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
        GetComponent<Button>().interactable = associatedSpell.IsAvailable();
        bar.gameObject.SetActive(!associatedSpell.IsAvailable());
        bar.Set(associatedSpell.GetRemainingTime() / associatedSpell.GetRequiredTime());
    }

    void SetButtonSprite(Spell spell)
    {
        if (spell.GetSprite())
        {
            GetComponentInChildren<Text>().gameObject.SetActive(false);
            image.sprite = spell.GetSprite();
        }
        else
        {
            GetComponentInChildren<Text>().text = "";
            image.gameObject.SetActive(false);
        }
    }
}
