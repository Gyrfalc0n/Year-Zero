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

    void Start()
    {
        activated = false;
        associatedButton = transform.Find("Button").GetComponent<Button>();
        associatedButton.onClick.AddListener(Buy);
        InitNextSkills();
    }

    public void Buy()
    {
        if (PlayerManager.playerManager.Pay(new int[] { 0, 0, 0, cost }, 0, false))
        {
            if (transform.parent.GetComponent<Skill>() != null && transform.parent.GetComponent<Skill>().restrictive)
            {
                transform.parent.GetComponent<Skill>().LockChildren();
            }
            else
                Lock();
            associatedButton.GetComponent<Image>().color = Color.green;
            UnlockChildren();
            Effect();
            activated = true;
            SelectUnit.selectUnit.ReloadTools();
        }
    }

    void InitNextSkills()
    {
        foreach (Transform skill in transform)
        {
            if (skill.name != "Button" && skill.GetComponent<Skill>() != null)
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
