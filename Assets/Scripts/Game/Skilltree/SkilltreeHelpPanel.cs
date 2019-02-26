using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkilltreeHelpPanel : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    [SerializeField]
    Text skillName;
    [SerializeField]
    Text description;
    [SerializeField]
    ResourceTextDescrPanel cost;

    public void Show(Skill skill)
    {
        obj.SetActive(true);
        skillName.text = skill.skillName;
        description.text = skill.description;
        cost.value.text = skill.cost.ToString();
    }

    public void Hide()
    {
        obj.SetActive(false);
    }
}
