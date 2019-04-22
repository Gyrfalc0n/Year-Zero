using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonoDescriptionPanel : MonoBehaviour
{
    [SerializeField]
    Text nameText;
    [SerializeField]
    GameObject obj;
    [SerializeField]
    Text resourceText;

    DestructibleUnit associatedUnit;

    public void Init(DestructibleUnit unit)
    {
        obj.SetActive(true);
        associatedUnit = unit;
        nameText.text = unit.objName;
        SetResourceText(associatedUnit);
    }

    void Update()
    {
        if (obj.activeInHierarchy && associatedUnit != null)
        {
            SetResourceText(associatedUnit);
        }
    }

    void SetResourceText(DestructibleUnit unit)
    {
        if (unit.GetComponent<BuilderUnit>() != null)
        {
            string s = string.Empty;
            if (unit.GetComponent<MiningSystem>().GetLastResource() == MiningSystem.Resources.FOOD)
            {
                s = "Food";
            }
            else if (unit.GetComponent<MiningSystem>().GetLastResource() == MiningSystem.Resources.ORE)
            {
                s = "Ore";
            }
            if (s != string.Empty)
                resourceText.text = s + " : " + unit.GetComponent<MiningSystem>().GetResourceAmount();
            else
                resourceText.text = "";
        }

    }

    public void ResetBar()
    {
        obj.SetActive(false);
    }
}
