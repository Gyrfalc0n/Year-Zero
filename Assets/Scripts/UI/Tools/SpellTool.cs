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
