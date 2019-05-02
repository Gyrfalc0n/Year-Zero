using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardsPanel : MonoBehaviour {

    [SerializeField]
    Card cardPrefab;
    [SerializeField]
    private Transform content;

    public void ClearCards()
    {
        while (content.childCount > 0)
        {
            Transform child = content.GetChild(0);
            child.SetParent(null);
            Destroy(child.gameObject);
        }
    }

    public void CheckCards()
    {
        if (SelectUnit.selectUnit.selected.Count > 0)
        {
            ShowCardsList(SelectUnit.selectUnit.selected);
        }
    }

    public void ShowCardsList(List<SelectableObj> list)
    {
        ClearCards();
        foreach (SelectableObj unit in list)
        {
            if (unit.GetComponent<MovableUnit>() != null)
            {
                Card tmp = Instantiate(cardPrefab, content);
                tmp.Init(unit.GetComponent<MovableUnit>());
            }
        }
        SelectCard(0);
    }

    public void SelectCard(int index = -1)
    {
        Transform tmp;
        if (index == -1)
        {
            tmp = EventSystem.current.currentSelectedGameObject.transform;
        }
        else
        {
            tmp = content.GetChild(index);
        }

        int counter = 0;
        foreach (Transform child in content)
        {
            if (child.gameObject == tmp.gameObject)
            {
                child.GetComponent<Card>().Highlight();
                SelectUnit.selectUnit.ChangeUnderSelected(counter);
            }
            else
            {
                child.GetComponent<Card>().DeHighlight();
            }
            counter++;
        }
    }

    public void UpdateCards()
    {
        foreach (Transform child in content)
        {
            child.GetComponent<Card>().UpdateCard();
        }
    }
}