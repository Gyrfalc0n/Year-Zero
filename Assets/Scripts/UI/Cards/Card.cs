using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

    [SerializeField]
    GameObject hl;
    [SerializeField]
    Image lifeBar;
    MovableUnit associatedUnit;
    [SerializeField] Image image;

    public void Init(MovableUnit unit)
    {
        associatedUnit = unit;
        if (associatedUnit.iconSprite != null)
        {
            image.gameObject.SetActive(true);
            image.sprite = associatedUnit.iconSprite;
        }
    }

    public void OnClicked()
    {
        GetComponentInParent<CardsPanel>().SelectCard();
    }

    void Update()
    {
        lifeBar.fillAmount = associatedUnit.GetLife() / associatedUnit.GetMaxlife();
        if (associatedUnit.GetLife() <= 0)
            Destroy(this);
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
