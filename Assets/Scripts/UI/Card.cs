using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour {

    [SerializeField]
    private GameObject hl;

    public void OnClicked()
    {
        GetComponentInParent<CardsPanel>().SelectCard();
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
