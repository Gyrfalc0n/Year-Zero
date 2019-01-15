using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using ExitGames.Client.Photon;

public class PlayerSettings : MonoBehaviourPunCallbacks
{
    Hashtable customProp;
    [SerializeField]
    Text playerName;
    [SerializeField]
    Dropdown raceDropdown;
    [SerializeField]
    Dropdown teamDropdown;
    [SerializeField]
    Dropdown colorDropdown;

    void Awake()
    {
        if (photonView.IsMine)
        {
            playerName.text = PhotonNetwork.NickName;
            raceDropdown.interactable = true;
            teamDropdown.interactable = true;
        }
        else
        {
            playerName.text = photonView.Owner.NickName;
            raceDropdown.interactable = false;
            teamDropdown.interactable = false;
        }

        if (PhotonNetwork.IsMasterClient)
        {
            colorDropdown.interactable = true;
        }
        else
        {
            colorDropdown.interactable = false;
        }

        UpdateProps();
    }

    public void OnValueChanged()
    {
        UpdateProps();
    }

    void UpdateProps()
    {
        customProp = PhotonNetwork.LocalPlayer.CustomProperties;
        ChangeProp("Race", raceDropdown.value);
        ChangeProp("Team", teamDropdown.value);
        ChangeProp("Color", colorDropdown.value);
        PhotonNetwork.LocalPlayer.SetCustomProperties(customProp);
    }

    void ChangeProp(string prop, int value)
    {
        if (customProp.ContainsKey(prop))
        {
            customProp[prop] = value;
        }
        else
        {
            customProp.Add(prop, value);
        }
    }
}
