using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SingleplayerMenu : MonoBehaviour
{
    [SerializeField] public GameObject WarningMissionNotCleared;

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

    public void StartEndlessMode()
    {
        if ((PlayerPrefs.GetInt("missionCleared", 0) == 1))
        {
            PhotonNetwork.LoadLevel("EndlessMode");
            FindObjectOfType<AudioManager>().PlaySound("BattleMusic");
        }
        else
        {
            WarningMissionNotCleared.GetComponent<TemporaryMenuMessage>().Activate();
        }
    }
}
