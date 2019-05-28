using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    Button associatedButton;
    bool activated;

    public bool restrictive;
    public List<Skill> forcedNext = new List<Skill>();

    public int cost;

    List<Skill> nextSkills = new List<Skill>();

    public string skillName;
    [TextArea(3, 5)]
    public string description;

    [SerializeField] Sprite activatedSprite;


    void Start()
    {
        activated = false;
        associatedButton = transform.Find("Button").GetComponent<Button>();
        associatedButton.onClick.AddListener(Buy);
        InitNextSkills();
    }

    public void Buy()
    {
        if (PlayerManager.playerManager.Pay(new int[] { 0, 0, 0, cost }, 0, true))
        {
            if (transform.parent.GetComponent<Skill>() != null && transform.parent.GetComponent<Skill>().restrictive)
            {
                transform.parent.GetComponent<Skill>().LockChildren();
            }
            else
                Lock();
            ChangeSprite();
            UnlockChildren();
            Effect();
            activated = true;
            SelectUnit.selectUnit.ReloadTools();
            MusicStage();
        }
    }

    private void MusicStage() // change la musique pour correspondre à l'ambiance de la partie en fonction de l'avancée technologique
    {
        switch (SkilltreeManager.manager.tier)
        {
            case 0:
                if (SkilltreeManager.manager.amountPaid >= 200)
                {
                    FindObjectOfType<AudioManager>().PlaySound("14. Sigma Tauri" );
                    SkilltreeManager.manager.tier++;
                }
                break;
            case 1:
                if (SkilltreeManager.manager.amountPaid >= 500)
                {
                    FindObjectOfType<AudioManager>().PlayRandomSound(
                        new []{"01. Stellaris Suite (Creation & Beyond)","04. To the Ends of the Galaxy (Instrumental)","08. Alpha Centauri"} );
                    SkilltreeManager.manager.tier++;
                }
                break;
            case 2:
                if (SkilltreeManager.manager.amountPaid >= 1250)
                {
                    FindObjectOfType<AudioManager>().PlayRandomSound(new []{"03. Deep Space Travels","05. In Search of Life","12. Pillars of Creation"} );
                    SkilltreeManager.manager.tier++;
                }
                break;
            case 3:
                if (SkilltreeManager.manager.amountPaid >= 2250)
                {
                    FindObjectOfType<AudioManager>().PlayRandomSound(new []{"02. Faster Than Light","05. In Search of Life","19. Distant Nebula"} );
                    SkilltreeManager.manager.tier++;
                }
                break;
            case 4:
                if (SkilltreeManager.manager.amountPaid >= 3500)
                {
                    FindObjectOfType<AudioManager>().PlayRandomSound(new []{"05. In Search of Life","15. Journey Through the Galaxy"} );
                    SkilltreeManager.manager.tier++;
                }
                break;
            case 5:
                if (SkilltreeManager.manager.amountPaid >= 5000)
                {
                    FindObjectOfType<AudioManager>().PlayRandomSound(new []{"18. Luminescence","10. The Celestial City","11. Infinite Being (Instrumental)"} );
                    SkilltreeManager.manager.tier++;
                }
                break;
            
        }
        
    }
    

    void ChangeSprite()
    {
        if (!activatedSprite)
        {
            associatedButton.GetComponent<Image>().color = Color.green;
        }
        else
        {
            associatedButton.GetComponent<Image>().sprite = activatedSprite;
        }

    }

    void InitNextSkills()
    {
        foreach (Transform skill in transform)
        {
            if (skill.name != "Image"  && skill.name != "Button" && skill.GetComponent<Skill>() != null)
            {
                nextSkills.Add(skill.GetComponent<Skill>());
            }
        }
        foreach (Skill skill in forcedNext)
        {
            nextSkills.Add(skill);
        }
    }

    public void Lock()
    {
        associatedButton.interactable = false;
    }

    public void Unlock()
    {
        if (!activated)
            associatedButton.interactable = true;
    }

    void UnlockChildren()
    {
        foreach (Skill skill in nextSkills)
        {
            skill.Unlock();
        }
    }

    void LockChildren()
    {
        foreach (Skill skill in nextSkills)
        {
            skill.Lock();
        }
    }

    public virtual void Effect() { }
}
