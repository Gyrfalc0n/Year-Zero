using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleplayerMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject createGameMenu;

    [SerializeField]
    private GameObject CampaignMenu;

    public void Campaign()
    {
        CampaignMenu.SetActive(true);
        gameObject.SetActive(false);
    }

    public void QuickMatch()
    {
        createGameMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
