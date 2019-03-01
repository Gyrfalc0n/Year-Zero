﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    [SerializeField]
    GameObject hl;
    [SerializeField]
    Image lifeBar;
    MovableUnit associatedUnit;

    public void Init(MovableUnit unit)
    {
        associatedUnit = unit;
    }

    public void OnClicked()
    {
        GetComponentInParent<CardsPanel>().SelectCard();
    }

    void Update()
    {
        lifeBar.fillAmount = associatedUnit.GetLife() / associatedUnit.GetMaxlife();
    }

    public void Highlight()
    {
        hl.SetActive(true);
    }

    public void DeHighlight()
    {
        hl.SetActive(false);
    }
}
