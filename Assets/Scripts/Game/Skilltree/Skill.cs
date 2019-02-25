using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    Button associatedButton;
    public bool activatedByDefault;
    bool activated;

    public bool restrictive;
    public List<Skill> forcedNext = new List<Skill>();

    public int cost;

    List<Skill> nextSkills = new List<Skill>();

    void Start()
    {
        activated = activatedByDefault;
        associatedButton = GetComponent<Button>();
        InitNextSkills();
    }

    public void Buy()
    {
        if (PlayerManager.playerManager.Pay(new int[] { 0, 0, 0, cost }, 0))
        {
            if (transform.parent.GetComponent<Skill>() != null && transform.parent.GetComponent<Skill>().restrictive)
            {
                transform.parent.GetComponent<Skill>().LockChildren();
            }
            else
                Lock();
            UnlockChildren();
            Effect();
            activated = true;
        }
        else
        {
            Debug.Log("Not enough resources");
            //Not enough resources Message
        }
    }

    void InitNextSkills()
    {
        foreach (Transform skill in transform)
        {
            if (skill.GetComponent<Skill>() != null)
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
