using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillHelpModule : MonoBehaviour
{
    SkilltreeHelpPanel panel;
    void Start()
    {
        panel = GameObject.Find("SkilltreeHelpPanel").GetComponent<SkilltreeHelpPanel>();
    }

    public void OnPointerEnter()
    {
        panel.Show(GetComponent<Skill>());
    }

    public void OnPointerExit()
    {
        panel.Hide();
    }
}
