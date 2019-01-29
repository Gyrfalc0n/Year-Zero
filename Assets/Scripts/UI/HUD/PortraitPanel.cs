using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PortraitPanel : MonoBehaviour
{
    [SerializeField]
    Text lifeText;

    DestructibleUnit associatedUnit;

    public void Init(DestructibleUnit obj)
    {
        associatedUnit = obj;
    }

    void Update()
    {
        if (associatedUnit != null)
            lifeText.text = associatedUnit.GetLife() + "\\" + associatedUnit.GetMaxlife();
    }
}
